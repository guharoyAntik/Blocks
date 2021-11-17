using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Image _screenFadePanel;
    [SerializeField] private Font _font;
    private float _fadeTime = 1f;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        Application.targetFrameRate = 60;
        // _font.material.mainTexture.filterMode = FilterMode.Point;
        DontDestroyOnLoad(this.gameObject);
    }

    public async void GameOver()
    {
        SoundManager.Instance.PlayGameOverSound();
        await SlowFadeIn();
        //insert small ads
        InGameUIManager.Instance.ShowGameOverPanel();
        await FadeOut();
    }

    #region Open Scenes
    public async void MainMenu()
    {
        await FadeIn();
        SceneManager.LoadScene(0);
        await FadeOut();
    }

    public async void SettingsMenu()
    {
        await FadeIn();
        SceneManager.LoadScene(1);
        await FadeOut();
    }

    public async void PlayGame()
    {
        await FadeIn();
        SceneManager.LoadScene(2);
        await FadeOut();
    }
    #endregion

    #region Transistions
    public async Task FadeIn()
    {
        _screenFadePanel.DOFade(0f, 0f).SetEase(Ease.InQuad);
        _screenFadePanel.gameObject.SetActive(true);
        await _screenFadePanel.DOFade(1f, _fadeTime).AsyncWaitForCompletion();
    }

    public async Task FadeOut()
    {
        _screenFadePanel.DOFade(1f, 0f).SetEase(Ease.InQuad);
        await _screenFadePanel.DOFade(0f, _fadeTime).AsyncWaitForCompletion();
        _screenFadePanel.gameObject.SetActive(false);
    }

    public async Task SlowFadeIn()
    {
        _screenFadePanel.DOFade(0f, 0f).SetEase(Ease.InQuad);
        _screenFadePanel.gameObject.SetActive(true);
        await _screenFadePanel.DOFade(1f, _fadeTime * 1.5f).AsyncWaitForCompletion();
    }
    #endregion
}
