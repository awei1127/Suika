using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int score;
    private int totalScore;
    public BallGenerator ballGenerator;
    public BallScores ballScores;
    

    void Start()
    {
        // 訂閱球產生器的創建球事件
        // 改用單例模式
        ballGenerator.SpawnBall += SpawnBallHandler;
    }

    // 創建球事件處理器 事件發生就訂閱手上的球的同球碰撞事件
    void SpawnBallHandler(object sender, SpawnBallEventArgs e)
    {
        e.spawnedBallCollisionHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // 同球碰撞事件處理器 事件發生就計分
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        score = ballScores.scores.FirstOrDefault(ballscore => ballscore.ballNumber == e.ballNumber)?.score ?? 0;
        totalScore += score;
        Debug.Log("目前總分：" + totalScore);
    }
}
