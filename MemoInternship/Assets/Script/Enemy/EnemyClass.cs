using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {stop,walk,attack,die}
public enum EnemyType {BrainMonster,EyeMonster,Boomer}
public class EnemyClass : MonoBehaviour
{
    public float EnemyHp;
    public float EnemyAtk;
    public EnemyState enemyState = EnemyState.walk;
    public EnemyType enemyType;
    public virtual void EnemyWalk()
    {
        Debug.Log("Walking");
    }

    public virtual void EnemyAttack()
    {
        Debug.Log("Attack"); 
    }

    public virtual void EnemyDie()
    {
        Debug.Log("Die");
    }

    public virtual void EnemyStop()
    {
        Debug.Log("Stop");
    }
}
