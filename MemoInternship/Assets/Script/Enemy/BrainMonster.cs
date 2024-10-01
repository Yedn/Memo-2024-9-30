using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMonster : EnemyClass
{
    // Start is called before the first frame update
    public GameObject brainMonster;
    public Transform BrainMonsterTransform;
    public float MoveSpeed;
    void Start()
    {
        enemyType = EnemyType.BrainMonster;
        EnemyHp = 15.0f;
        EnemyAtk = 2.0f;
        MoveSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case (EnemyState.walk):
                {
                    EnemyWalk();
                    break;
                }
            case (EnemyState.attack):
                {
                    EnemyAttack();
                    break;
                }
            case (EnemyState.die):
                {
                    EnemyDie();
                    break;
                }
        }
    }

    public override void EnemyWalk()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
        {
            Debug.Log("ERROR");
            return;
        }
        Vector3 direction = (target.transform.position - BrainMonsterTransform.position);
        if (target.transform.position.x > BrainMonsterTransform.position.x)
        {
            brainMonster.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            brainMonster.GetComponent<SpriteRenderer>().flipX = true;
        }
        BrainMonsterTransform.Translate((direction.normalized) * MoveSpeed * Time.deltaTime);
        if (direction.magnitude < 0.05f)
        {
            enemyState = EnemyState.attack;
        }
    }
    public override void EnemyAttack()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            Vector3 direction = (target.transform.position - BrainMonsterTransform.position);
            if (direction.magnitude >= 0.5f)
            {
                enemyState = EnemyState.walk;
            }
        }
        else
        {
            Debug.Log("No Target");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 collisionPosition = new Vector2(collision.GetComponent<Transform>().position.x, collision.GetComponent<Transform>().position.y);
            Vector2 RepelledDirection = collisionPosition - new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
            //collision.GetComponent<PlayerController>().PlayerRepelled(RepelledDirection.normalized);
            collision.GetComponent<PlayerController>().GetHit(EnemyAtk);
        }
        if (collision.tag == "Bullet")
        {
            EnemyHp -= collision.GetComponent<Bullet>().atkValue;
            StartCoroutine("hitFlash");
            if (EnemyHp <= 0)
            {
                enemyState = EnemyState.die;
            }
        }
    }

    IEnumerator hitFlash()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void EnemyDie()
    {
        this.GetComponent<Animator>().SetTrigger("Die");
        this.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject,1.5f);
    }


}
