using System;
using UnityEngine.SceneManagement;

public class GameSceneManger : Singleton<GameSceneManger>
{
    public Action onSceneLoaded;

    public void ChangeScene(string sceneName)
    {
        if (sceneName != null)
        {
            onSceneLoaded?.Invoke();
            SceneManager.LoadScene(sceneName);
        }
    }
}
