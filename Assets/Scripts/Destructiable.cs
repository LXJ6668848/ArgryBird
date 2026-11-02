using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Destructiable : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public List<Sprite> injuredSpriteList;

    private SpriteRenderer spriteRenderer;

    private GameObject boomPrefab;

    private void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();

        boomPrefab = Resources.Load<GameObject>("Boom1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.relativeVelocity.magnitude)
        TakeDamage((int)(collision.relativeVelocity.magnitude * 9));
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Dead();
        }
        else
        {
            Sprite beforeSprite = spriteRenderer.sprite;
            int index = (int)((maxHP - currentHP) / (maxHP / (injuredSpriteList.Count + 1.0f))) - 1;
            if (index >= 0)
            {
                spriteRenderer.sprite = injuredSpriteList[index];
            }
            if (beforeSprite != spriteRenderer.sprite)
            {
                PlayAudioCollision();
            }
        }
    }

    protected virtual void PlayAudioCollision()
    {
        AudioManager.Instance.PlayWoodCollision(transform.position);
    }

    protected virtual void PlayAudioDestroyed()
    {
        AudioManager.Instance.PlayWoodDestoryed(transform.position);
    }

    public virtual void Dead()
    {
        PlayAudioDestroyed();
        GameObject.Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
