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
        // �q�\�y���;����Ыزy�ƥ�
        // ��γ�ҼҦ�
        ballGenerator.SpawnBall += SpawnBallHandler;
    }

    // �Ыزy�ƥ�B�z�� �ƥ�o�ʹN�q�\��W���y���P�y�I���ƥ�
    void SpawnBallHandler(object sender, SpawnBallEventArgs e)
    {
        e.spawnedBallCollisionHandler.SameBallCollided += SameBallCollidedHandler;
    }

    // �P�y�I���ƥ�B�z�� �ƥ�o�ʹN�p��
    void SameBallCollidedHandler(object sender, SameBallCollidedEventArgs e)
    {
        score = ballScores.scores.FirstOrDefault(ballscore => ballscore.ballNumber == e.ballNumber)?.score ?? 0;
        totalScore += score;
        Debug.Log("�ثe�`���G" + totalScore);
    }
}
