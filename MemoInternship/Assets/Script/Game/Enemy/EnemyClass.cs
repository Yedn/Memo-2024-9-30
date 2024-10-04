using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyState {stop,walk,attack,die,pause}
public enum EnemyType {BrainMonster,EyeMonster,Boomer}

public class EnemyClass : MonoBehaviour
{
    public float EnemyHp;
    public float EnemyAtk;
    public EnemyState enemyState = EnemyState.walk;
    public EnemyType enemyType;
    public GameObject direction;
    public float distance;

    public List<GameObject> DropsPrefabList;

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

    public void BuildDrop()
    {
        int DropsNum = Random.Range(0, 6);
        for (; DropsNum > 0; DropsNum --)
        {
            int DropsType = Random.Range(0, DropsPrefabList.Count);
            float jumpDistance = Random.Range(0.1f, 0.35f);
            jumpDistance = Random.Range(0, 2) < 1 ? -jumpDistance : jumpDistance;
            Vector3 position = transform.position; 
            position.x += jumpDistance;
            GameObject go = GameObject.Instantiate(DropsPrefabList[DropsType], transform.position, Quaternion.identity);
            go.GetComponent<Drops>().JumpTo(position);
        }
    }

}
