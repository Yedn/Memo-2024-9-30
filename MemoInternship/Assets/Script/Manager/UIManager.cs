using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public List<GameObject> HeartList;
    public List<Image> HpUIList;
    public Sprite NullHeartImage;

    public TextMeshProUGUI RestTimeText;
    public int restTime;
    public float timer;

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI LoseText;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        ShowRestTime();
        //if (GameManager.instance.restTime > 0)
        //{
        //    ShowRestTime();
        //}
    }
    public void ShowRestTime()
    {
        int M = (int)(GameManager.instance.restTime / 60);
        float S = GameManager.instance.restTime % 60;
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            GameManager.instance.restTime -= 1;
            RestTimeText.text = M + ":" + string.Format("{0:00}", S);
        }
    }

    public void ShowStartText()
    {
        WinText.enabled = false;
        LoseText.enabled = false;
    }

    public void ShowWinText()
    {
        WinText.enabled = true;
        LoseText.enabled = false;
    }

    public void ShowLoseText()
    {
        WinText.enabled = false;
        LoseText.enabled = true;
    }

    public void ChangeHpUI(float currentHp)
    {
        int HpNum = (int)currentHp;
        if (HpNum < HpUIList.Count)
        {
            Debug.Log(HpNum.ToString());
            HpUIList[HpNum].sprite.GetComponent<Animator>().SetTrigger("Null");
            HpUIList[HpNum].sprite = NullHeartImage;
            //HeartList[HpNum].GetComponent<Image>().sprite = NullHeartImage;
            //HeartList[HpNum].GetComponent<Animator>().SetTrigger("Null");
        }
    }
}
