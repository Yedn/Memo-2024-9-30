using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Playerstate { game, die, pause }
public enum CanShoot { OK, NotOK }

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public Transform playerTransform;
    public float speed = 5.0f;
    public GameObject weapon;
    public Playerstate playerstate = Playerstate.pause;
    public Animator animator;


    public float Hp = 5.0f;

    [Header("Attack")]
    public CanShoot canShoot = CanShoot.OK;
    public int MaxBulletNum = 24;
    public float atkValue = 2.5f;
    public float atkDuration = 0.3f;
    private float shootTime = 0.0f;
    public Bullet bulletPrefab;
    public float bulletSpeed = 10.0f;
    public int CurrentBulletNum;
    public float BulletRecoverDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = CanShoot.OK;
        CurrentBulletNum = MaxBulletNum;
        playerTransform = this.transform;
        TranslateToGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerstate)
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
        WeaponMove();
        BulletRecover();
        Attack();
        LookAt();
    }

    public void Attack()
    {
        shootTime += Time.deltaTime;
        if (shootTime >= atkDuration)
        {
            if (Input.GetMouseButtonDown(0) && canShoot == CanShoot.OK)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;
                float fireAngle = Vector2.Angle(mousePosition - this.transform.position, Vector2.up);
                if (mousePosition.x > this.transform.position.x)
                {
                    fireAngle = -fireAngle;
                }
                Bullet go = GameObject.Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
                CurrentBulletNum -= 1;
                if (CurrentBulletNum <= 0)
                {
                    canShoot = CanShoot.NotOK;
                    BulletRecover();
                }
                go.GetComponent<Rigidbody2D>().velocity = ((mousePosition - transform.position).normalized * bulletSpeed);
                go.transform.eulerAngles = new Vector3(0, 0, fireAngle);
                shootTime = 0.0f;
            }
        }
    }

    public void BulletRecover()
    {
        if (Input.GetKeyDown("r") || CurrentBulletNum == 0)
        {
            canShoot = CanShoot.NotOK;
            while (CurrentBulletNum < MaxBulletNum)
            {
                StartCoroutine("RecoverBullet");
                if (CurrentBulletNum > 0 && Input.GetMouseButtonDown(0))
                {
                    canShoot = CanShoot.OK;
                    break;
                }
            }
            canShoot = CanShoot.OK;
        }
    }

    IEnumerator RecoverBullet()
    {
        CurrentBulletNum += 1;
        yield return new WaitForSeconds(5.0f);
    }

    public void WeaponMove()
    {
        weapon.GetComponent<Transform>().position = new Vector3(0.068f, 0, 0) + playerTransform.position;
    }

    public void PlayerMove()
    {
        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            playerTransform.Translate(new Vector3(-1, 1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", true);
            animator.SetBool("Ddown", false);
            //weapon.GetComponent<Transform>().Translate(new Vector3(-1, 1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("w") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(1, 1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", false);
            animator.SetBool("Ddown", true);
            //weapon.GetComponent<Transform>().Translate(new Vector3(1, 1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", false);
            animator.SetBool("Ddown", true);
            //weapon.GetComponent<Transform>().Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            playerTransform.Translate(new Vector3(1, -1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", true);
            animator.SetBool("Ddown", false);
            //weapon.GetComponent<Transform>().Translate(new Vector3(1, -1, 0) * speed * Time.deltaTime);

        }
        else
        {
            if (Input.GetKey("w"))
            {
                playerTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", false);
                animator.SetBool("Ddown", true);
                //weapon.GetComponent<Transform>().Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("s"))
            {
                playerTransform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", true);
                animator.SetBool("Ddown", false);
                //weapon.GetComponent<Transform>().Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                playerTransform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", true);
                animator.SetBool("Ddown", false);
                //weapon.GetComponent<Transform>().Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }
            else if (Input.GetKey("d"))
            {
                playerTransform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", false);
                animator.SetBool("Ddown", true);
                //weapon.GetComponent<Transform>().Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Adown", false);
                animator.SetBool("Ddown", false);
            }
        }

    }
    public void LookAt()
    {
        Vector2 mouseposition;
        mouseposition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector2 direction = new Vector2(mouseposition.x - GetComponent<Transform>().position.x, mouseposition.y - GetComponent<Transform>().position.y);
        float angle = Vector2.Angle(direction, Vector2.right);
        if (mouseposition.y < this.transform.position.y)
            angle = -angle;
        weapon.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, angle);

        if (weapon.GetComponent<Transform>().rotation.z <= 90.0f && weapon.GetComponent<Transform>().rotation.z >= -90.0f)
        {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            weapon.GetComponent<SpriteRenderer>().flipY = true;
        }
    }
    public void GetHit(float atk)
    {
        Hp -= atk;
        if (Hp <= 0)
        {
            TranslateToDie();
        }
    }
}

