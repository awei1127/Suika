using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;
    public GameState CurrentGameState { get; private set; }
    private BallGenerator ballGenerator;

    void Awake()
    {
        // ��ҼҦ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // �q�\�y���;�����Ҥƨƥ� �H�K�ޥι��
        BallGenerator.BallGeneratorCreated += BallGeneratorCreatedEventHandler;
    }

    // �y���;���ҤƳB�z��
    private void BallGeneratorCreatedEventHandler(BallGenerator generator)
    {
        // �ޥγЫئn���y���;�
        ballGenerator = generator;
        // �q�\�y���;����Ыزy�ƥ�
        ballGenerator.SpawnBall += SpawnBallEventHandler;
    }

    // �Ыزy�ƥ�B�z��
    private void SpawnBallEventHandler(object sender, SpawnBallEventArgs e)
    {
        // �q�\�Ыت��y����F�W�t�ƥ�
        e.spawnedBallPositionHandler.ReachUpEdge += ReachUpEdgeEventHandler;
    }

    // ��F�W�t�ƥ�B�z��
    private void ReachUpEdgeEventHandler()
    {
        EndGame();
    }

    public void UpdateGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState)
        {
            return;
        }

        CurrentGameState = newGameState;

    }

    private void EndGame()
    {
        UpdateGameState(GameState.GameOver);
        Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
    }

}
