using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    public void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        anim.SetBool("isShow",true);
    }

    public void OnContinueButtonClick()
    {
        Time .timeScale = 1;
        anim.SetBool("isShow", false);
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
