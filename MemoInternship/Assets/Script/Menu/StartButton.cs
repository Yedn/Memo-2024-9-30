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
        //��ʲô�¶���
    }
    //����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = startImage.sprite;
        //��ʲô�¶���
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
