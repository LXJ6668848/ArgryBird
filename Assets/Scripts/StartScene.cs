using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator  Start()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }

}
