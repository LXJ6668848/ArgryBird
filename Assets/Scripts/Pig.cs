using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;

public class Pig : Destructiable
{
    public int score = 3000;

    public Vector3 pigPos;

    public override void Dead()
    {
        pigPos = transform.position;
        base.Dead();
        GameManager.Instance.OnPigDead();
        ScoreManager.Instance.ShowScore(pigPos, score);
    }

    override protected void PlayAudioCollision()
    {
        AudioManager.Instance.PlayPigCollison(transform.position);
    }

    override protected void PlayAudioDestroyed()
    {
        AudioManager.Instance.PlayPigCollison(transform.position);
    }
}
