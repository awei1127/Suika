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
        // 單例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 訂閱球產生器的實例化事件 以便引用實例
        BallGenerator.BallGeneratorCreated += BallGeneratorCreatedEventHandler;
    }

    // 球產生器實例化處理器
    private void BallGeneratorCreatedEventHandler(BallGenerator generator)
    {
        // 引用創建好的球產生器
        ballGenerator = generator;
        // 訂閱球產生器的創建球事件
        ballGenerator.SpawnBall += SpawnBallEventHandler;
    }

    // 創建球事件處理器
    private void SpawnBallEventHandler(object sender, SpawnBallEventArgs e)
    {
        // 訂閱創建的球的到達上緣事件
        e.spawnedBallPositionHandler.ReachUpEdge += ReachUpEdgeEventHandler;
    }

    // 到達上緣事件處理器
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
