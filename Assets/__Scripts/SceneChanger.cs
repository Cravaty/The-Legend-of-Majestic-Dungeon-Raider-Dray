using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void BackManu()
    {
        SceneManager.LoadScene(0);
    }

}
