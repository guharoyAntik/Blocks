using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject _gameOverPanel;

    //private Button _restartButton;

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;

    private int _score;
    private int _highScore;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;

        //Initializing Score
        _score = 0;
        _scoreText.text = _score.ToString();

        //Initializing HighScore
        _highScore = (PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0);
        _highScoreText.text = _highScore.ToString();
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateScore(int clearedCells)
    {
        int scoreUpdate = Random.Range(2, 4);

        int x = clearedCells * clearedCells;

        if (x % 10 != 0)
        {
            x = ((x / 10) + 1) * 10;
        }
        scoreUpdate += x;

        _score += scoreUpdate;
        if (_score > _highScore)
        {
            _highScore = _score;
            //_highScoreText.text = _highScore.ToString();
            PlayerPrefs.SetInt("HighScore", _highScore);
        }


        //slide numbers
        _scoreText.text = _score.ToString();
    }
}
