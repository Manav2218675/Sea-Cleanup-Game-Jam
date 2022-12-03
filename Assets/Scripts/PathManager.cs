using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathManager : MonoBehaviour
{
    public static PathManager pathManager;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(pathManager == null)
        {
            pathManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneID);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
