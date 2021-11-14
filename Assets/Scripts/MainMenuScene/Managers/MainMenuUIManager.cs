using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameManager.Instance.PlayGame();
    }

    public void OpenSettingsMenu()
    {
        SoundManager.Instance.PlayButtonClickSound();
        GameManager.Instance.SettingsMenu();
    }

    public void ExitGame()
    {
        SoundManager.Instance.PlayButtonClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
# else
        Application.Quit();
#endif
    }
}
