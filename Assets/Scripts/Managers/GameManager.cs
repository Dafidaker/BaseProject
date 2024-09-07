using System;
using Core;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        TransitionSceneManager.Instance.ActivateDontDestroyOnLoad();
        TransitionSceneManager.Instance.FadeToNextScene(5);
    }
}
