using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBackGround : MonoBehaviour
{
    public Animator anima;
    public void Awake()
    {
        anima = GetComponent<Animator>();
    }
    public void OnMouseEnter()
    {
        anima.SetTrigger("In");
    }
    //Êó±êÀë¿ª
    public void OnMouseExit()
    {
        anima.SetTrigger("Exit");
    }
}
