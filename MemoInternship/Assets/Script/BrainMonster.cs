using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMonster : EnemyClass
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyHp = 10.0f;
        EnemyAtk = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case (EnemyState.walk):
                {
                    break;
                }
            case (EnemyState.attack):
                {
                    break;
                }
            case (EnemyState.die):
                {
                    break;
                }
        }
    }
}
