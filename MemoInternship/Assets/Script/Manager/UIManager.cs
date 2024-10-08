using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("HPUI")]
    public List<GameObject> HeartList;
    [Header("TimeUI")]
    public TextMeshProUGUI RestTimeText;
    public int restTime;
    public float timer;
    [Header("ResultUI")]
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject ConclusionText;
    public GameObject ReturnButton;
    public GameObject ExitButton;
    public List<GameObject> ButtonBackGroundList;
    [Header("BulletUI")]
    public TextMeshProUGUI BulletText;
    [Header("LevelUI")]
    public TextMeshProUGUI LevelText;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        ShowRestTime();
        ChangeHpUI(GameObject.FindWithTag("Player").GetComponent<PlayerController>().Hp);
    }
    public void ShowRestTime()
    {
        int M = (int)(GameManager.instance.restTime / 60);
        float S = GameManager.instance.restTime % 60;
        timer += Time.deltaTime;
        if (timer >= 1f && GameManager.instance.restTime >= 0)
        {
            timer = 0;
            GameManager.instance.restTime -= 1;
            RestTimeText.text = M + ":" + string.Format("{0:00}", S);
        }
        if (GameManager.instance.restTime == 0 && GameManager.instance.gameState == GameState.Gameing)
        {
            GameManager.instance.GameWin();
        }
    }

    public void BulletNumUI(int Num)
    {
        BulletText.text = Num.ToString() + " / 24";
        if (Num == 0)
        {
            BulletText.color = Color.red;
        }
        else
        {
            BulletText.color = Color.white;
        }
    }

    public void ShowStartText()
    {
        WinText.SetActive(false);
        LoseText.SetActive(false);

    }

    public void ReturnToMenu()
    {
        ReturnButton.SetActive(true);
        ExitButton.SetActive(true);
        foreach (GameObject background in ButtonBackGroundList)
        {
            background.SetActive(true);
        }
    }

    public void ReturnToGame()
    {
        ReturnButton.SetActive(false);
        ExitButton.SetActive(false);
        foreach (GameObject background in ButtonBackGroundList)
        {
            background.SetActive(false);
        }
    }

    public void ShowResult()
    {
        ConclusionText.GetComponent<Text>().text = "GetExperience : " + GameObject.FindWithTag("Player").GetComponent<PlayerController>().AllExperience.ToString() + " Round : " + GameManager.instance.CurrentLevel.ToString();
        ConclusionText.SetActive(true);
    }
    public void ShowWinText()
    {
        WinText.SetActive(true);
        LoseText.SetActive(false);
    }

    public void ShowLoseText()
    {
        WinText.SetActive(false);
        LoseText.SetActive(true);
    }

    public void ChangeHpUI(float currentHp)
    {
        int HpNum = (int)currentHp;
        for (int i = 0;i<5;i++)
        {
            if (i < HpNum)
            {
                HeartList[i].transform.Find("Heart").gameObject.GetComponent<Animator>().SetTrigger("Recover");
            }
            else
            {
                HeartList[HpNum].transform.Find("Heart").gameObject.GetComponent<Animator>().SetTrigger("Null");
            }
        }
    }
    public void ChangeLevelUI()
    {
        LevelText.text = "Phace: " + GameManager.instance.CurrentLevel.ToString();
    }
}
