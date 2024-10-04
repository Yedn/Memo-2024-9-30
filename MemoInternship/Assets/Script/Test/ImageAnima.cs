using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ImageAnima : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isIn;

    public void Start()
    {
        isIn = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isIn == false)
        {
            isIn = true;
            this.GetComponent<Animator>().SetTrigger("In");
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isIn == true)
        {
            isIn = false;
            this.GetComponent<Animator>().SetTrigger("Out");
        }
    }
}
