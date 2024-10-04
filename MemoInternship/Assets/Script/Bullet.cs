using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Seting")]
    public float atkValue = 2.5f;
    public float bulletSpeed = 10.0f;
    public GameObject bulletDestoryPrefab;
    public Vector2 shootPoint;

    public void Start()
    {
        atkValue = GameObject.FindWithTag("Player").GetComponent<PlayerController>().atkValue;
        Destroy(gameObject, 9.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
            //collision.GetComponent<EnemyClass>().GetHit(collision.GetComponent<Bullet>().atkValue);
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab, transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
        }
        if (collision.tag == "Tree")
        {
            Destroy(this.gameObject);
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab, transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
        }
    }
}
