using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GameResult { Unknow,Win,Lose}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<Tilemap> BackGroundList;
    public float GameTime = 20.0f;
    public GameResult gameResult = GameResult.Unknow;
    public int totalTime = 20*60;
    public int restTime = 0;
    public TextMeshProUGUI WinText;
    public GameObject PlayerInWhichMap;
    private void Awake()
    {
        instance = this;
        restTime = totalTime;
    }

    void CreateBackGround()
    {

    }

    public void GameWin()
    {
        UIManager.Instance.ShowWinText();
    }

    public void GameLose()
    {
        UIManager.Instance.ShowLoseText();
    }
    public void Update()
    {
        
    }
}
