using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drops : MonoBehaviour
{
    public GameObject icon;
    public float StayTime = 30f;
    public float CurrentTime;
    private void Start()
    {
        CurrentTime = 0.0f;
    }

    public void Update()
    {
        StayUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<Animator>().SetTrigger("Free");
            Destroy(this, 0.5f);
        }
        GetBuff();
    }

    public virtual void GetBuff()
    {
        Debug.Log("GetBuff");
    }

    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1;
        Vector3 centerPos = (transform.position + targetPos) / 2;
        float distance = Vector3.Distance(transform.position, targetPos);
        centerPos.y += (distance / 2);
        //路径，时间，曲线类型(曲线平滑，SetEase:先快后慢
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos }, 0.5f, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    public virtual void StayUpdate()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= StayTime)
        {
            this.GetComponent<Animator>().SetTrigger("Free");
            Destroy(this, 0.5f);
            CurrentTime = 0.0f;
        }
    }
}
