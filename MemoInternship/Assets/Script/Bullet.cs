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
        Destroy(gameObject, 9.0f);
    }

    public void SetatkValue(float atkValue)
    {
        this.atkValue = atkValue;
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    public void SetShootPoint(Vector2 shootPoint)
    {
        this.shootPoint = shootPoint;
    }

    private void Update()
    {
        transform.Translate(shootPoint * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enamy")
        {
            Destroy(this.gameObject);
            //Õâ±ßÒª×ö¿ÛÑª
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab, transform.position, Quaternion.identity);
            Destroy(go, 1.0f);
        }
        if (collision.tag == "Tree")
        {
            Destroy(this.gameObject);
            GameObject go = GameObject.Instantiate(bulletDestoryPrefab, transform.position, Quaternion.identity);
            Destroy(go, 1.0f);
        }
    }
}
