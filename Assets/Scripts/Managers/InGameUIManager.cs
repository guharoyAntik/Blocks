using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _continueButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void ShowGameOverPanel()
    {
        _continueButton.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        _gameOverPanel.SetActive(true);
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void ContinueGame()
    {
        _gameOverPanel.SetActive(false);
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void RestartGame()
    {
        GameManager.Instance.PlayGame();
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void OpenMainMenu()
    {
        GameManager.Instance.MainMenu();
        SoundManager.Instance.PlayButtonClickSound();
    }
}
