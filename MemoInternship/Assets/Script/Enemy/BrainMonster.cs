using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMonster : EnemyClass
{
    // Start is called before the first frame update
    public GameObject brainMonster;
    public Transform BrainMonsterTransform;
    public float MoveSpeed;

    public float AttackDuration = 3.0f;
    public float AttackTime = 0.0f;
    void Start()
    {
        enemyState = EnemyState.walk;
        direction = GameObject.FindWithTag("Player");
        enemyType = EnemyType.BrainMonster;
        EnemyHp = 15.0f;
        EnemyAtk = 2.0f;
        MoveSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FindDistance();
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
        if (direction == null)
        {
            Debug.Log("ERROR");
            return;
        }
        Vector3 target = (direction.transform.position - BrainMonsterTransform.position);
        if (direction.transform.position.x > BrainMonsterTransform.position.x)
        {
            brainMonster.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            brainMonster.GetComponent<SpriteRenderer>().flipX = true;
        }
        BrainMonsterTransform.Translate((target.normalized) * MoveSpeed * Time.deltaTime);
        if (distance < 0.05f)
        {
            enemyState = EnemyState.attack;
        }
    }
    public override void EnemyAttack()
    {
        if (direction)
        {
            if (distance >= 0.5f)
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
        AttackTime = AttackDuration;
        if (collision.tag == "Player")
        {
            AttackTime += Time.deltaTime;
            while (AttackTime >= AttackDuration)
            {
                Debug.Log("Get Hit Form: BrainMonster");
                Vector2 collisionPosition = new Vector2(collision.GetComponent<Transform>().position.x, collision.GetComponent<Transform>().position.y);
                Vector2 RepelledDirection = collisionPosition - new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
                //collision.GetComponent<PlayerController>().PlayerRepelled(RepelledDirection.normalized);
                collision.GetComponent<PlayerController>().GetHit(EnemyAtk);
                AttackTime = 0.0f;
            }
        }
        if (collision.tag == "Bullet")
        {
            GetHit(collision.GetComponent<Bullet>().atkValue);
        }
    }

    public override void GetHit(float AtkValue)
    {
        EnemyHp -= AtkValue;
        StartCoroutine("hitFlash");
        if (EnemyHp <= 0)
        {
            enemyState = EnemyState.die;
        }
    }

    public override void EnemyDie()
    {
        this.GetComponent<Animator>().SetTrigger("Die");
        this.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject,1.5f);
    }


}
