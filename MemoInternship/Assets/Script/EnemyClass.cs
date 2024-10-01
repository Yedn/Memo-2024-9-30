using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {walk,attack,die}

public class EnemyClass : MonoBehaviour
{
    public float EnemyHp;
    public float EnemyAtk;
    public EnemyState enemyState = EnemyState.walk;

    public virtual void EnemyWalk()
    {
        Debug.Log("Walking");
    }

}
