using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public float boomRadius = 2f;

    protected override void FullTimeSkill()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boomRadius);
        foreach(Collider2D collider in colliders)
        {
            Destructiable des = collider.GetComponent<Destructiable>();
            if (des != null)
            {
                des.TakeDamage(10000);
            }
        }
        state = BirdState.WaitToDie;
        trail.enabled = false;
        trail.Clear();
        LoadNextBird();
    }
}
