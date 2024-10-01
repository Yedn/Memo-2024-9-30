using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boomer : EnemyClass
{
    public float moveSpeed = 4.0f;
    public Vector3 target;
    public Transform BoomerTransform;
    public GameObject BoomerPrefab;
    public GameObject BoomerRadius;
    public GameObject BoomerUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        EnemyHp = 3.0f;
        EnemyAtk = 3.0f;
        enemyType = EnemyType.Boomer;
        enemyState = EnemyState.walk;
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform.position;
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
        Vector3 direction = (target- BoomerTransform.position);
        if (target.x > BoomerTransform.position.x)
        {
            BoomerPrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            BoomerPrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
        BoomerTransform.Translate((direction.normalized) * moveSpeed * Time.deltaTime);
        if (direction.magnitude < 0.05f)
        {
            enemyState = EnemyState.attack;
        }
    }

    public override void EnemyAttack()
    {

        enemyState = EnemyState.die;
        Destroy(this.gameObject);
        GameObject go = GameObject.Instantiate(BoomerUIPrefab, transform.position,Quaternion.identity);
        Destroy(go, 0.5f);
    }
}
