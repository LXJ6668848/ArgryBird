using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameSO gameSO;

    private Bird[] birdList;
    private int index = -1;
    private int pigTotalCount;
    private int pigDeadCount;
    public GameOverUI gameOverUI;

    private FollowTarget cameraFollowTarget;

    private void Awake()
    {
        Instance = this;
        pigDeadCount = 0;
        LoadSelectedLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        birdList = FindObjectsByType<Bird>(FindObjectsSortMode.None);
        pigTotalCount = FindObjectsByType<Pig>(FindObjectsSortMode.None).Length;
        cameraFollowTarget = Camera.main.GetComponent<FollowTarget>();
        LoadNextBird();
    }

    private void LoadSelectedLevel()
    {
        Time.timeScale = 1.0f;
        int mapID = gameSO.selectedMapID;
        int levelID = gameSO.selectedLevelID;
        GameObject levelPrefab = Resources.Load<GameObject>("Map" + mapID + "/" + "Level" + levelID);
        GameObject.Instantiate(levelPrefab);
    }
    
    public void LoadNextBird()
    {
        index++;
        if(index >= birdList.Length)
        {
            GameOver();
        }
        else
        {
            birdList[index].GoStage(Slingshot.Instance.getCenterPosition());
            cameraFollowTarget.SetTarget(birdList[index].transform);
        }

    }

    public void OnPigDead()
    {
        pigDeadCount++;
        if(pigDeadCount >= pigTotalCount)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        int starCount = 0;
        float pigDeadPercent = (float) (pigDeadCount * 1.000f / pigTotalCount);
        
        if(pigDeadPercent > 0.33f)
        {
            starCount = 1;
        }
        if (pigDeadPercent > 0.66f)
        {
            starCount++;
        }
        if (pigDeadPercent > 0.99f)
        {
            starCount++;
        }
        gameOverUI.Show(starCount);

        gameSO.UpdateLevel(starCount);
    }

    public void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelList()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
