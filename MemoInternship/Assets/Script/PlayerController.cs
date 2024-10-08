using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    public float Hp = 10.0f;

    [Header("Attack")]
    public CanShoot canShoot = CanShoot.OK;
    public int MaxBulletNum = 24;
    public float atkValue = 2.5f;
    public float atkDuration = 0.3f;
    private float shootTime = 0.0f;
    public Bullet bulletPrefab;
    public float bulletSpeed = 10.0f;
    public int CurrentBulletNum;
    public float BulletRecoverDuration = 4.0f;
    public float BulletRecoverCurrentTime = 0.0f;

    [Header("Skill")]
    public float skillDurationTime = 10.0f;
    public float skillTime = 0.0f;

    [Header("PlayerLevel")]
    private int PlayerLevel = 1;

    public float NeedExperience = 10.0f;
    public float HaveExperience = 0.0f;
    public float AllExperience = 0.0f;
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

                    break;
                }
            default:
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
        if (GameManager.instance.restTime > 0)
        {
            GameManager.instance.gameResult = GameResult.Lose;
            GameManager.instance.GameLose();
        }
    }

    public void GameingUpdate()
    {
        PlayerMove();
        WeaponMove();
        BulletRecover();
        LookAt();
        Attack();
        SkillRecover();
        LevelUp();
    }

    public void LevelUp()
    {
        if (NeedExperience <= HaveExperience)
        {
            AudioManager.instance.PlayClip(Config.levelup);
            PlayerLevel += 1;
            if (Hp < 5)
            {
                Hp = 5;
                AudioManager.instance.PlayClip(Config.hp_recover);
            }
            if (atkValue < 5.0f)
            {
                atkValue += 0.1f;
            }
            HaveExperience -= NeedExperience;
            NeedExperience += 5.0f;
        }
    }

    public void SkillRecover()
    {
        if (skillTime < skillDurationTime)
        {
            skillTime += Time.deltaTime;
        }
        if (skillTime >= skillDurationTime)
        {
            UIManager.Instance.BulletText.color = Color.yellow;
        }

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

                Bullet go = GameObject.Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
                AudioManager.instance.PlayClip(Config.shoot,1.7f);
                CurrentBulletNum -= 1;
                UIManager.Instance.BulletNumUI(CurrentBulletNum);
                if (CurrentBulletNum <= 0)
                {
                    canShoot = CanShoot.NotOK;
                    BulletRecoverCurrentTime = 0.0f;
                }
                go.GetComponent<Rigidbody2D>().velocity = ((mousePosition - transform.position).normalized * bulletSpeed);
                shootTime = 0.0f;
            }
        }
        if (Input.GetKeyDown("r"))
        {
            canShoot = CanShoot.NotOK;
            BulletRecoverCurrentTime = 0.0f;
        }
        if (Input.GetKeyDown("c") && skillTime >= skillDurationTime)
        {
            Skill();
            canShoot = CanShoot.NotOK;
            skillTime = 0.0f;
        }
    }

    public void Skill()
    {
        for (; CurrentBulletNum >= 0; CurrentBulletNum--)
        {
            shootTime = 0.0f;
            float angle = Random.Range(0, Mathf.PI);
            Bullet go = GameObject.Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = ((new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)).normalized * bulletSpeed);
            while (shootTime < 0.5f)
            {
                shootTime += Time.deltaTime;
            }
        }
    }
    public void BulletRecover()
    {

        if (canShoot == CanShoot.NotOK)
        {
            RecoverBullet_UsedDeltatime();
            if ((CurrentBulletNum == MaxBulletNum) || (Input.GetMouseButtonDown(0) && CurrentBulletNum > 0))
            {
                canShoot = CanShoot.OK;
            }
        }
    }


    public void RecoverBullet_UsedDeltatime()
    {
        BulletRecoverCurrentTime += Time.deltaTime;
        if (BulletRecoverCurrentTime > BulletRecoverDuration)
        {
            if (CurrentBulletNum < MaxBulletNum)
            {
                CurrentBulletNum++;
                AudioManager.instance.PlayClip(Config.bullet_recover);
                UIManager.Instance.BulletNumUI(CurrentBulletNum);
            }
            else
            {
                canShoot = CanShoot.OK;
            }
            BulletRecoverCurrentTime = 0;
        }
    }

    private void PlayWalkSound()
    {
        int soundplay = Random.Range(0, 2);
        if (soundplay == 0)
        {
            AudioManager.instance.PlayClip(Config.walk_1);
        }
        else
        {
            AudioManager.instance.PlayClip(Config.walk_2);
        }
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
            int soundplay = Random.Range(0, 2);
            PlayWalkSound();
        }
        else if (Input.GetKey("w") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(1, 1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", false);
            animator.SetBool("Ddown", true);
            PlayWalkSound();
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            playerTransform.Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", false);
            animator.SetBool("Ddown", true);
            PlayWalkSound();
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            playerTransform.Translate(new Vector3(1, -1, 0) * speed * Time.deltaTime);
            animator.SetBool("Adown", true);
            animator.SetBool("Ddown", false);
            PlayWalkSound();
        }
        else
        {
            if (Input.GetKey("w"))
            {
                playerTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", false);
                animator.SetBool("Ddown", true);
                PlayWalkSound();
            }
            else if (Input.GetKey("s"))
            {
                playerTransform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", true);
                animator.SetBool("Ddown", false);
                PlayWalkSound();
            }
            else if (Input.GetKey("a"))
            {
                playerTransform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", true);
                animator.SetBool("Ddown", false);
                PlayWalkSound();
            }
            else if (Input.GetKey("d"))
            {
                playerTransform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                animator.SetBool("Adown", false);
                animator.SetBool("Ddown", true);
                PlayWalkSound();
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
        if (playerstate == Playerstate.game)
        {
            StartCoroutine("hitFlash");
            if (Hp <= 0)
            {
                TranslateToDie();
            }
        }

    }
    IEnumerator hitFlash()
    {
        Hp -= 1;
        UIManager.Instance.ChangeHpUI(Hp);
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.75f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

