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
        if (GameManager.instance.restTime == 0 && GameManager.instance.gameResult != GameResult.Win)
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
        if (HpNum < HeartList.Count)
        {
            HeartList[HpNum].transform.Find("Heart").gameObject.GetComponent<Animator>().SetTrigger("Null");
        }
    }
    public void ChangeLevelUI()
    {
        LevelText.text = "Level: " + GameManager.instance.CurrentLevel.ToString();
    }
}
