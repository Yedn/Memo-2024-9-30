using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public enum GameResult { Unknow,Win,Lose}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

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

    private void Awake()
    {
        instance = this;
        restTime = totalTime;
        player = GameObject.FindWithTag("Player");
    }
    
    public void GameWin()
    {
        UIManager.Instance.ShowWinText();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
    }

    public void GameLose()
    {
        UIManager.Instance.ShowLoseText();
    }

}
