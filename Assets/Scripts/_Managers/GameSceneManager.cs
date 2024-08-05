using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public enum Scene
    {
        MainScene,
        MainMenu
    }

    public static void ChangeScene(Scene scene)
    {
        TransitionManager.Instance.StartTransition();

        FunctionTimer.Create(() =>
        {
            SceneManager.LoadScene(scene.ToString());
        }, 1f);
    }
}
