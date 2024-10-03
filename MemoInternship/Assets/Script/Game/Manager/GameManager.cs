using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public enum GameState { Gameing,Pause,End}
public enum GameResult { Unknow,Win,Lose}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameState gameState = GameState.Pause;
    public List<GameObject> BackGroundList;
    public Tilemap tileMap;
    public float GameTime = 20.0f;
    public GameResult gameResult = GameResult.Unknow;
    public int totalTime = 20*60;
    public int restTime = 0;
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
    private void Awake()
    {
        instance = this;
        restTime = totalTime;
        player = GameObject.FindWithTag("Player");
        gameState = GameState.Gameing;
    }

    public void Update()
    {
        if (gameState == GameState.Gameing)
        {
            Gameing();
        }
    }

    public void GameWin()
    {
        UIManager.Instance.ShowWinText();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
        gameState = GameState.End;
    }

    public void GameLose()
    {
        UIManager.Instance.ShowLoseText();
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
        for (int i=0;i<MaxEnemyNum;i++)
        {
            float angle = 0, radius = 0;
            float x, y;
            Vector2 playerpos = new Vector2(playerTransform.position.x, playerTransform.position.y);
            int EnemyType = Random.Range(0,3);
            angle = Random.Range(0, 2 * Mathf.PI);
            radius = Random.Range(3.0f, 7.0f);
            x = (float)Mathf.Cos(angle) * radius;y= (float)Mathf.Sin(angle) * radius;
            foreach(Vector2 preenemy in EnemyPosList)
            {
                while ((new Vector2(x,y)-preenemy).magnitude < 1.0f)
                {
                    angle = Random.Range(0, 2 * Mathf.PI);
                    radius = Random.Range(3.0f, 7.0f);
                    x = (float)Mathf.Cos(angle) * radius; y = (float)Mathf.Sin(angle) * radius;
                }
            }
            GameObject go = GameObject.Instantiate(MonsterPrefabList[EnemyType], new Vector3(x, y, 0), Quaternion.identity);
            EnemyList.Add(go.GetComponent<EnemyClass>());
            EnemyPosList.Add(new Vector2(x, y));
        }
    }

    public void CreateEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            float x1 = 0; float y1 = 0;
            MonsterNumber = Random.Range(2, 8);
            for (int j = 0; j < MonsterNumber; j++)
            {
                int LeftRightUpDown = Random.Range(0, 4);//左右上下
                switch (LeftRightUpDown)
                {
                    case 0: x1 = GameObject.FindWithTag("Player").transform.position.x - 9.5f; y1 = Random.Range(-8.5f, +8.5f); break;
                    case 1: x1 = GameObject.FindWithTag("Player").transform.position.x + 9.5f; y1 = Random.Range(-8.5f, +8.5f); break;
                    case 2: y1 = GameObject.FindWithTag("Player").transform.position.y + 5.5f; x1 = Random.Range(-10.0f, +10.0f); break;
                    case 3: y1 = GameObject.FindWithTag("Player").transform.position.y - 5.5f; x1 = Random.Range(-10.0f, +10.0f); break;
                }
                if (i != 1)
                {
                    foreach (Vector2 prepos in EnemyPosList)
                    {
                        Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                        while (((x1 <= prepos.x + 0.5f && x1 >= prepos.x - 0.5f) && (y1 <= prepos.y + 2.0f && y1 >= prepos.y - 2.0f)) || ((new Vector2(x1, y1) - playerPos).magnitude < 3.0f || (new Vector2(x1, y1) - playerPos).magnitude > 6.0f))
                        {
                            switch (LeftRightUpDown)
                            {
                                case 0: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 1: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 2: x1 = Random.Range(-10.0f, +10.0f); break;
                                case 3: x1 = Random.Range(-10.0f, +10.0f); break;
                            }
                        }
                    }
                }
                else if (i == 1)//远程怪一定在视野范围内
                {
                    foreach (Vector2 prepos in EnemyPosList)
                    {
                        Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                        while (((x1 <= prepos.x + 0.5f && x1 >= prepos.x - 0.5f) && (y1 <= prepos.y + 2.0f && y1 >= prepos.y - 2.0f)) || ((new Vector2(x1, y1) - playerPos).magnitude < 3.0f || (new Vector2(x1, y1) - playerPos).magnitude > 6.0f))
                        {
                            switch (LeftRightUpDown)
                            {
                                case 0: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 1: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 2: x1 = Random.Range(-10.0f, +10.0f); break;
                                case 3: x1 = Random.Range(-10.0f, +10.0f); break;
                            }
                        }
                    }
                }
                GameObject go = GameObject.Instantiate(MonsterPrefabList[i], new Vector3(x1, y1, 0), Quaternion.identity);
                EnemyList.Add(go.GetComponent<EnemyClass>());
                EnemyPosList.Add(new Vector2(x1, y1));
            }
        }
    }

}
