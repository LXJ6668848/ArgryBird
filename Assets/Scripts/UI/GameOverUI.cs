using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private Animator anim;

    private int starCount = 0;

    public GameObject failPig;

    public StarUI StarUI1;
    public StarUI StarUI2;
    public StarUI StarUI3;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Show(int starCount)
    {
        anim.SetTrigger("IsShow");

        this.starCount = starCount;

    }

    public void ShowStar()
    {
        if(starCount == 0)
        {
            failPig.SetActive(true);
        }
        if(starCount >= 1)
        {
            StarUI1.Show();
        }
        if(starCount >= 2)
        {
            StarUI2.Show();
        }
        if(starCount >= 3)
        {
            StarUI3.Show();
        }
    }

    public void OnRestartButtonClick()
    {
        GameManager.Instance.RestartLevel();
    }

    public void OnLevelListButtonClick()
    {
        GameManager.Instance.LevelList();
    }
}
