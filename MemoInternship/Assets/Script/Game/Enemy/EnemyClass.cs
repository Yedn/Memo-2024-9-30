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
    public GameObject direction;
    public float distance;

    public void Awake()
    {
        direction = GameObject.FindWithTag("Player");
        if (direction == null )
        {
            Debug.Log("error");
        }

    }
    public void FindDistance()//怪物和玩家的距离
    {
        distance = (direction.transform.position - this.transform.position).magnitude;
    }
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

    public virtual void GetHit(float AtkValue)
    {
        Debug.Log("GetHit");
    }

    IEnumerator hitFlash()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
