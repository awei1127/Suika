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

            // 訂閱場景載入完畢事件 以便之後在場景載入完畢時 引用球產生器
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // 導演物件被刪除時 取消訂閱場景載入完畢事件
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 場景載入完畢事件處理器
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // 引用創建好的球產生器
        ballGenerator = FindObjectOfType<BallGenerator>();

        if (ballGenerator != null)
        {
            // 訂閱球產生器的創建球事件
            ballGenerator.SpawnBall += SpawnBallEventHandler;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // 創建球事件處理器
    private void SpawnBallEventHandler(object sender, SpawnBallEventArgs e)
    {
        // 訂閱創建的球的到達上緣事件
        e.spawnedBallPositionHandler.ReachUpEdge += ReachUpEdgeEventHandler;
    }

    // 到達上緣事件處理器
    private void ReachUpEdgeEventHandler(object sender, EventArgs e)
    {
        // 取得發生事件的物件 嘗試將 sender 轉型為 ballPositionHandler 類型
        BallPositionHandler ballPositionHandler = sender as BallPositionHandler;

        // 如果轉型成功 (如果發生事件的物件是 BallPositionHandler 類型)
        if (ballPositionHandler != null)
        {
            // 取消訂閱上緣事件 以避免重複執行 EndGame
            ballPositionHandler.ReachUpEdge -= ReachUpEdgeEventHandler;
        }

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
        Time.timeScale = 0f;
        MenuView.Instance.GameOverPanel.SetActive(true);
        MenuView.Instance.InputPanel.SetActive(false);
        UpdateGameState(GameState.GameOver);
    }
}
