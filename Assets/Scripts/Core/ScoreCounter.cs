using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int score;
    public int TotalScore { get; private set; }
    public BallGenerator ballGenerator;
    public BallScores ballScores;
    public event Action<int> ScoreChanged;


    void Awake()
    {
        // 訂閱球產生器的創建球事件
        ballGenerator.SpawnBall += SpawnBallHandler;
    }

    // 創建球事件處理器 事件發生就訂閱手上的球的同球碰撞事件
    void SpawnBallHandler(object sender, SpawnBallEventArgs e)
    {
        e.spawnedBallCollisionHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 同球碰撞事件處理器 事件發生就計分 並發生分數更新事件
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        score = ballScores.scores.FirstOrDefault(ballscore => ballscore.ballNumber == e.ballNumber)?.score ?? 0;
        TotalScore += score;
        Debug.Log("目前總分：" + TotalScore);
        OnScoreChanged(TotalScore);
    }

    void OnScoreChanged(int totalScore)
    {
        ScoreChanged?.Invoke(totalScore);
    }
}
