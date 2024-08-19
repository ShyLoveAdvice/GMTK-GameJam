using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    public string gameSceneName;
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
