using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer_Radius : MonoBehaviour
{
    public Boomer boomerBody;
    public float EnemyAtk=2.0f;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (boomerBody.enemyState == EnemyState.attack)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("Get Hit Form: Boomer");
                collision.GetComponent<PlayerController>().GetHit(EnemyAtk);
            }
            else if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyClass>().GetHit(EnemyAtk);
            }
            boomerBody.enemyState = EnemyState.die;
        }
    }
}
