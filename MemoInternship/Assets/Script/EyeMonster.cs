using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EyeMonster : EnemyClass
{
    public GameObject eyeMonster;
    public EnemyBullet BulletPrefab;
    public Transform EyeMonsterTransform;
    public float MoveSpeed;
    public float MaxRadius = 10.0f;
    public float MinRadius = 3.0f;
    public float BulletSpeed = 3.0f;

    public float shootDuration = 5.0f;
    public float shootTime = 0.0f;

    void Start()
    {
        enemyType = EnemyType.EyeMonster;
        EnemyHp = 8.0f;
        EnemyAtk = 1.0f;
        MoveSpeed = 2.5f;
    }

    void Update()
    {
        FindDistance();
        SetState();
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
            case (EnemyState.stop):
                {
                    EnemyStop();
                    break;
                }
        }
    }

    private void SetState()
    {
        if (enemyState != EnemyState.die)
        {
            if (distance <= MaxRadius && distance > MinRadius)
            {
                enemyState = EnemyState.walk;
            }
            else if (distance > MaxRadius)
            {
                enemyState = EnemyState.stop;
            }
            else
            {
                enemyState = EnemyState.attack;
            }
        }
    }

    public override void EnemyWalk()
    {
        Vector3 target = (direction.transform.position - EyeMonsterTransform.position);
        if (target.x > EyeMonsterTransform.position.x)
        {
            eyeMonster.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            eyeMonster.GetComponent<SpriteRenderer>().flipX = false;
        }
        EyeMonsterTransform.Translate((target.normalized) * MoveSpeed * Time.deltaTime);
    }

    public override void EnemyStop()
    {
        if (distance < MaxRadius)
        {
            enemyState = EnemyState.walk;
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

    public override void EnemyAttack()
    {
        shootTime += Time.deltaTime;
        while (shootTime >= shootDuration)
        {
            Vector3 target = direction.GetComponent<Transform>().position;
            target.z = 0;
            EnemyBullet go = GameObject.Instantiate(BulletPrefab, EyeMonsterTransform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = ((target - transform.position).normalized * BulletSpeed);
            shootTime = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GetHit(collision.GetComponent<Bullet>().atkValue);
        }
    }

    public override void EnemyDie()
    {
        GameManager.instance.EnemyList.Remove(this);
        this.GetComponent<Animator>().SetTrigger("Die");
        this.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.5f);
    }
}
