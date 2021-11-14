using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private int[] _scoreLevels = { 50, 200, 1000, 10000, 100000 };
    private int _score;
    private int _highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        Application.targetFrameRate = 60;

        //Initializing Score
        _score = 0;
        _scoreText.text = _score.ToString();

        //Initializing HighScore
        _highScore = (PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0);
        _highScoreText.text = _highScore.ToString();
    }

    private int GetScoreUpdate(int clearedCells)
    {
        if (clearedCells == 0)
        {
            return Random.Range(3, 7);
        }

        if (clearedCells == 4)
        {
            return _scoreLevels[0];
        }
        else if (clearedCells == 7 || clearedCells == 8)
        {
            return _scoreLevels[1];
        }
        else if (clearedCells == 9 || clearedCells == 10)
        {
            return _scoreLevels[2];
        }
        else if (clearedCells == 11 || clearedCells == 12)
        {
            return _scoreLevels[3];
        }

        Debug.Log("Invaid, cleared cells = " + clearedCells);
        return 0;
    }

    public async void UpdateScore(string clearedColor, int clearedCells)
    {
        int scoreUpdate = GetScoreUpdate(clearedCells);
        int scoreLevel = -1;

        for (int i = 0; i < _scoreLevels.Length; ++i)
        {
            if (scoreUpdate == _scoreLevels[i])
            {
                scoreLevel = i;
            }
        }

        // await InGameUIManager.Instance.ShowFlashMessage("red", 2);   //testing
        if (scoreLevel > 0)
        {
            await InGameUIManager.Instance.ShowFlashMessage(clearedColor, scoreLevel - 1);
        }
        StartCoroutine(SlideScore(_score + scoreUpdate));
    }

    IEnumerator SlideScore(int target)
    {
        int scoreDelta;
        float timeDelta = Time.deltaTime;
        float animationTime = 1f;

        scoreDelta = (int)System.Math.Ceiling((target * timeDelta) / animationTime);
        if (scoreDelta == 0)
        {
            scoreDelta = 1;
            Debug.Log("score delta zero");
        }
        while (_score < target)
        {
            _score += scoreDelta;
            if (_score > target)
            {
                _score = target;
            }
            _scoreText.text = _score.ToString();
            yield return new WaitForSeconds(timeDelta);
        }

        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }
}
