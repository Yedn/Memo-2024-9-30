using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Image otherImage;
    public Image startImage;
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = otherImage.sprite;
        //做什么事都行
    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = startImage.sprite;
        //做什么事都行
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
