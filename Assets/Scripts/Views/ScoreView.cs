using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreView : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ScoreCounter scoreCounter;
    public int currentScore;
    public int highestScore;
    
    void Start()
    {
        UpdateScore(0);
        scoreCounter.ScoreChanged += ScoreChangedHandler;
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
    }

}
