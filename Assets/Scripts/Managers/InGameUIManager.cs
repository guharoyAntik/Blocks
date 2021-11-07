using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    public GameObject UICanvas;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _highsScore;
    [SerializeField] private GameObject _holder;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _flashMessagePrefab;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameOverOptions;

    [Header("Pause Panel")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Image _pausePanelBackground;
    [SerializeField] private GameObject _pausePanelOptions;
    private float _pausePanelAnimationTime = 1f;

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
        //Set up layout for gameover panel
        _pauseButton.SetActive(false);
        _highsScore.SetActive(false);
        _holder.SetActive(false);

        //Align game over options
        float height = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        _gameOverOptions.transform.position = new Vector3(0, -2 * height / 3, 0);
        _score.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        //Align game over score
        Vector3 currentScorePos = _score.transform.localPosition;
        _score.transform.localPosition = new Vector3(currentScorePos.x, Screen.height * 0.5f * 0.5f, currentScorePos.z);
        _board.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        _gameOverPanel.SetActive(true);
    }

    public void ShowPauseGamePanel()     //Called through pause butoon in-game
    {
        //Pre opening sequence
        _pausePanelOptions.transform.localScale = Vector3.zero;
        _pausePanelBackground.DOFade(0f, 0f);
        _pausePanel.SetActive(true);

        SoundManager.Instance.PlayButtonClickSound();

        //Opening sequence
        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, _pausePanelBackground.DOFade(0.8f, _pausePanelAnimationTime));
        seq.Insert(0f, _pausePanelOptions.transform.DOScale(Vector3.one, _pausePanelAnimationTime));
        seq.SetEase(Ease.OutQuad);
        seq.Play();
    }

    public async Task ShowFlashMessage(string color, int level)
    {
        FlashMessage flashMessage = Instantiate(_flashMessagePrefab, _flashMessagePrefab.transform.position, _flashMessagePrefab.transform.rotation, _score.transform).GetComponent<FlashMessage>();
        flashMessage.transform.localPosition = Vector3.zero;
        flashMessage.SetFlashMessage(color, level);
        await flashMessage.ShowFlashMessage();
    }

    public async void ContinueGame()    //Pause menu continue option
    {
        SoundManager.Instance.PlayButtonClickSound();

        //Closing sequence
        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, _pausePanelBackground.DOFade(0f, _pausePanelAnimationTime));
        seq.Insert(0f, _pausePanelOptions.transform.DOScale(Vector3.zero, _pausePanelAnimationTime));
        seq.SetEase(Ease.OutQuad);
        await seq.Play().AsyncWaitForCompletion();

        //Post closing sequence
        _pausePanel.SetActive(false);
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
