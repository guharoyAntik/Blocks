using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver()
    {
        SoundManager.Instance.PlayGameOverSound();
        InGameUIManager.Instance.ShowGameOverPanel();
    }

    #region Open Scenes

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    #endregion
}
