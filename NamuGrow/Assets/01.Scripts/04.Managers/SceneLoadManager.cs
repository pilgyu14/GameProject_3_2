using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    public void LoadInGameScene()
    {
        SceneManager.LoadSceneAsync("InGame");
        SceneManager.LoadSceneAsync("UI_Test",LoadSceneMode.Additive);
    }
}
