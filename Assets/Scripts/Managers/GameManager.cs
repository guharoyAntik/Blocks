using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Image _screenFadePanel;
    private float _fadeTime = 0.5f / 2;

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

    public async void MainMenu()
    {
        //add scene fade
        _screenFadePanel.gameObject.SetActive(true);
        await _screenFadePanel.DOFade(1f, _fadeTime).AsyncWaitForCompletion();
        SceneManager.LoadScene(0);
        await _screenFadePanel.DOFade(0f, _fadeTime).AsyncWaitForCompletion();
        _screenFadePanel.gameObject.SetActive(false);
    }

    public async void SettingsMenu()
    {
        //add scene fade
        _screenFadePanel.gameObject.SetActive(true);
        await _screenFadePanel.DOFade(1f, _fadeTime).AsyncWaitForCompletion();
        SceneManager.LoadScene(1);
        await _screenFadePanel.DOFade(0f, _fadeTime).AsyncWaitForCompletion();
        _screenFadePanel.gameObject.SetActive(false);
    }

    public async void PlayGame()
    {
        //add scene fade
        _screenFadePanel.gameObject.SetActive(true);
        await _screenFadePanel.DOFade(1f, _fadeTime).AsyncWaitForCompletion();
        SceneManager.LoadScene(2);
        await _screenFadePanel.DOFade(0f, _fadeTime).AsyncWaitForCompletion();
        _screenFadePanel.gameObject.SetActive(false);
    }

    #endregion
}
