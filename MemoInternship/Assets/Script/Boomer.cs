using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boomer : EnemyClass
{
    public float moveSpeed = 3.5f;
    public Vector3 target;
    public Transform BoomerTransform;
    public GameObject BoomerPrefab;
    public GameObject BoomerRadius;
    public GameObject BoomerUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        BoomerRadius.GetComponent<Collider2D>().enabled = false;
        EnemyHp = 3.0f;
        EnemyAtk = 3.0f;
        enemyType = EnemyType.Boomer;
        enemyState = EnemyState.walk;
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
            case (EnemyState.pause):
                {
                    break;
                }
        }
    }

    public override void EnemyWalk()
    {
        Vector3 target = (direction.transform.position - BoomerTransform.position);
        if (target.x > BoomerTransform.position.x)
        {
            BoomerPrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            BoomerPrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
        BoomerTransform.Translate(target.normalized * moveSpeed * Time.deltaTime);
        if (distance < 1.0f)
        {
            enemyState = EnemyState.attack;
        }
    }

    public override void EnemyAttack()
    {
        BoomerRadius.GetComponent<Collider2D>().enabled = true;
        enemyState = EnemyState.die;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GetHit(collision.GetComponent<Bullet>().atkValue);
        }
    }
    public override void GetHit(float AtkValue)
    {
        EnemyHp -= AtkValue;
        StartCoroutine("hitFlash");
        if (EnemyHp <= 0 && enemyState != EnemyState.die)
        {
            BuildDrop();
            enemyState = EnemyState.die;
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
        GameManager.instance.EnemyList.Remove(this);
        Destroy(this.gameObject);
        GameObject go = GameObject.Instantiate(BoomerUIPrefab, transform.position,Quaternion.identity);
        Destroy(go, 0.5f);
    }
    
}
