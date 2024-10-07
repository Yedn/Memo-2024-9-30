using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public enum GameState { Gameing, Pause, End }//游戏状态
public enum GameResult { Unknow, Win, Lose }//游戏结果
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameState gameState = GameState.Pause;
    public List<GameObject> BackGroundList;
    public Tilemap tileMap;
    public float GameTime = 20.0f;
    public GameResult gameResult = GameResult.Unknow;
    public int totalTime = 20 * 60; //游戏倒计时20分钟
    public int restTime = 20 * 60;
    public TextMeshProUGUI WinText;

    public GameObject player;
    public Transform playerTransform;
    public GameObject MapPrefab;

    [Header("EnemyController")]
    public List<EnemyClass> EnemyList;
    public List<GameObject> MonsterPrefabList;
    public List<Vector2> EnemyPosList;
    public int MonsterNumber;

    [Header("LevelController")]
    public int MaxEnemyNum = 5;
    public int CurrentLevel = 1;
    public void Awake()
    {
        instance = this;
        restTime = totalTime;
        player = GameObject.FindWithTag("Player");
    }

    public void Start()
    {
        AudioManager.instance.PlayBgm(Config.bgm);
        gameState = GameState.Gameing;
        CreateEnemy_CircleWay();
    }

    public void Update()
    {
        if (gameState == GameState.Gameing)
        {
            Gameing();
        }
        if (gameState == GameState.Gameing && Input.GetKeyDown(KeyCode.Escape)) //游戏中按ESC 暂停
        {
            gameState = GameState.Pause;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
            foreach (EnemyClass enemy in EnemyList)
            {
                enemy.enemyState = EnemyState.pause;
            }
            UIManager.Instance.ReturnToMenu();
        }
        else if (gameState == GameState.Pause && Input.GetKeyDown(KeyCode.Escape)) //暂停时按ESC 继续游戏
        {
            gameState = GameState.Gameing;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.game;
            UIManager.Instance.ReturnToGame();
            foreach (EnemyClass enemy in EnemyList)
            {
                enemy.enemyState = EnemyState.walk;
            }
        }
    }

    public void GameWin() //游戏胜利
    {
        AudioManager.instance.PlayClip(Config.win_music);
        UIManager.Instance.ShowWinText(); //显示胜利
        UIManager.Instance.ReturnToMenu(); //放返回按钮
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
        gameState = GameState.End;
    }

    public void GameLose() //输了
    {
        AudioManager.instance.PlayClip(Config.lose_music);
        UIManager.Instance.ShowLoseText();
        UIManager.Instance.ReturnToMenu();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
        gameState = GameState.End;
    }

    public void Gameing()
    {
        if (restTime > 0 && EnemyList.Count == 0)
        {
            CurrentLevel += 1;
            MaxEnemyNum += 5;
            UIManager.Instance.ChangeLevelUI();
            CreateEnemy_CircleWay();
        }
    }

    public void CreateEnemy_CircleWay()
    {
        for (int i = 0; i < MaxEnemyNum; i++)
        {
            float angle = 0, radius = 0;
            float x, y;
            Vector2 playerpos = new Vector2(playerTransform.position.x, playerTransform.position.y);
            int EnemyType = Random.Range(0, 3);
            angle = Random.Range(0, 2 * Mathf.PI);
            radius = Random.Range(3.0f, 7.0f);
            x = (float)Mathf.Cos(angle) * radius; y = (float)Mathf.Sin(angle) * radius;
            foreach (Vector2 preenemy in EnemyPosList)
            {
                while ((new Vector2(x, y) - preenemy).magnitude < 3.0f)
                {
                    angle = Random.Range(0, 2 * Mathf.PI);
                    radius = Random.Range(3.0f, 7.0f);
                    x = (float)Mathf.Cos(angle) * radius; y = (float)Mathf.Sin(angle) * radius;
                }
            }
            GameObject go = GameObject.Instantiate(MonsterPrefabList[EnemyType], playerTransform.position + new Vector3(x, y, 0), Quaternion.identity);
            EnemyList.Add(go.GetComponent<EnemyClass>());
            EnemyPosList.Add(new Vector2(x, y));
        }
        EnemyPosList.Clear();
    }


}
