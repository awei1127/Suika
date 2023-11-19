using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreView : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundScoreText;
    public ScoreCounter scoreCounter;
    private int currentScore;
    private int highestScore;

    void Awake()
    {
        // 訂閱分數更新事件
        scoreCounter.ScoreChanged += ScoreChangedHandler;
    }

    void Start()
    {
        UpdateScore(0);
    }

    void UpdateScore(int newScore)
    {
        currentScore = newScore;
    }

    void UpdateHighestScore()
    {
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
        }
    }

    void ScoreChangedHandler(int newScore)
    {
        UpdateScore(newScore);
        scoreText.text = newScore.ToString();
        roundScoreText.text = newScore.ToString();
    }
}
