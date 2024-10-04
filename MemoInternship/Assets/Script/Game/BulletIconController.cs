using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIconController : Drops
{
    public override void GetBuff()
    {
        float currentBullet = GameObject.FindWithTag("Player").GetComponent<PlayerController>().CurrentBulletNum;
        float MaxBulletNum = GameObject.FindWithTag("Player").GetComponent<PlayerController>().MaxBulletNum;
        if (currentBullet < MaxBulletNum)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().CurrentBulletNum += 1;
        }
    }
}
