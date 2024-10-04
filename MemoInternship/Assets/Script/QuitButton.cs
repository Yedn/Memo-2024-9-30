using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
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
    public void QuitGame()
    {
        AudioManager.instance.PlayClip(Config.btn_click);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
