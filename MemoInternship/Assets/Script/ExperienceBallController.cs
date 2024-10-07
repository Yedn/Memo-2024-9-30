using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBallController : Drops
{
    public override void GetBuff()
    {
        AudioManager.instance.PlayClip(Config.get_experience);
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().HaveExperience += 1;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().AllExperience += 1;
    }
}
