using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Seting")]
    public float atkValue = 1.0f;
    public float bulletSpeed = 5.0f;
    public GameObject bulletDestoryPrefab;
    public Vector2 shootPoint;

    public void Start()
    {
        Destroy(gameObject, 9.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Debug.Log("BE SHOOT");
            Destroy(this.gameObject);
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab,this.transform.position,Quaternion.identity);
            collision.GetComponent<PlayerController>().GetHit(atkValue);
            Destroy(go, 0.5f);
        }
        else if (collision.tag == "Tree")
        {
            Destroy(this.gameObject);
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab, this.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
        }

    }
}
