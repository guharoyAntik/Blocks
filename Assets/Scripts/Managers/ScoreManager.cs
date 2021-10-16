using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;

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

    private int GetScoreChange(int clearedCells)
    {
        int scoreUpdate = Random.Range(2, 4);

        int x = clearedCells * clearedCells;

        if (x % 10 != 0)
        {
            x = ((x / 10) + 1) * 10;
        }
        scoreUpdate += x;

        return scoreUpdate;
    }

    public void UpdateScore(int clearedCells)
    {
        int scoreUpdate = GetScoreChange(clearedCells);

        StartCoroutine(SlideScore(_score + scoreUpdate));
    }

    IEnumerator SlideScore(int target)
    {
        int scoreDelta;
        float timeDelta = Time.deltaTime;
        float animationTime = 1f;

        scoreDelta = (int)System.Math.Ceiling((target * timeDelta) / animationTime);
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
