using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene("GameScene");
    }
    public void ReturnMenu()
    {
        AudioManager.instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene("MenuScene");
    }
    public void ExitGame()
    {
        AudioManager.instance.PlayClip(Config.btn_click);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
