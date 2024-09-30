using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Playerstate {game,die,pause}

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5.0f;
    public GameObject weapon;
    public Playerstate playerstate = Playerstate.pause;
    public float Hp = 5.0f;
    public float atkValue = 2.5f;
    public Bullet bulletPrefab;
    public float bulletSpeed=5.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        TranslateToGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerstate)
        {
            case Playerstate.game:
                {
                    GameingUpdate(); break;
                }
            case Playerstate.die:
                {
                    DieUpdate(); break;
                }
            case Playerstate.pause:
                {

                }
                break;
        }
    }

    public void TranslateToGame()
    {
        playerstate = Playerstate.game;
    }

    public void TranslateToDie()
    {
        playerstate = Playerstate.die;
    }

    public void DieUpdate()
    {

    }

    public void GameingUpdate()
    {
        PlayerMove();
        LookAt();
        if (Hp <= 0 )
        {
            TranslateToDie();
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet go = GameObject.Instantiate(bulletPrefab, weapon.GetComponent<Transform>().position, Quaternion.identity);
            go.SetatkValue(atkValue);
            go.SetBulletSpeed(bulletSpeed);
        }
    }
    public void PlayerMove()
    {
        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            playerTransform.Translate(new Vector3(-1, 1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("w") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(1, 1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            playerTransform.Translate(new Vector3(1, -1, 0) * speed * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey("w"))
            {
                playerTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("s"))
            {
                playerTransform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                playerTransform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("d"))
            {
                playerTransform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            }
        }
    }

    public void LookAt()
    {
        Vector2 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        point = Camera.main.ScreenToWorldPoint(point);
        weapon.GetComponent<Transform>().LookAt(point, Vector3.back);
    }
}
