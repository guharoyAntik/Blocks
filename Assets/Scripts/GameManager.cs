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
    private Button _restartButton;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _score;

    private void Awake()
    {
        Instance = this;
        _score = 0;
        Application.targetFrameRate = 60;
    }

    public void ShowRestartButton()
    {
        _restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateScore(int clearedCells)
    {
        int x = clearedCells * clearedCells;

        if (x % 10 != 0)
        {
            x = ((x / 10) + 1) * 10;
        }
        _score += x;


        _scoreText.text = _score.ToString();
    }
}
