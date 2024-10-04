using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SoulIconController : Drops
{
    public override void GetBuff()
    {
        float currentHP = GameObject.FindWithTag("Player").GetComponent<PlayerController>().Hp;
        float MaxHp = 5.0f;
        if (currentHP <= MaxHp - 1)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().Hp += 1;
            //UIManager.Instance.HeartList[(int)(currentHP)].transform.Find("Heart").gameObject.GetComponent<Animator>().SetTrigger("Recover");
            AudioManager.instance.PlayClip(Config.hp_recover);
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().Hp = MaxHp;
        }
    }
}
