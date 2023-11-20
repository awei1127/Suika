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

            // �q�\�������J�����ƥ� �H�K����b�������J������ �ޥβy���;�
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // �ɺt����Q�R���� �����q�\�������J�����ƥ�
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �������J�����ƥ�B�z��
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // �ޥγЫئn���y���;�
        ballGenerator = FindObjectOfType<BallGenerator>();

        if (ballGenerator != null)
        {
            // �q�\�y���;����Ыزy�ƥ�
            ballGenerator.SpawnBall += SpawnBallEventHandler;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // �Ыزy�ƥ�B�z��
    private void SpawnBallEventHandler(object sender, SpawnBallEventArgs e)
    {
        // �q�\�Ыت��y����F�W�t�ƥ�
        e.spawnedBallPositionHandler.ReachUpEdge += ReachUpEdgeEventHandler;
    }

    // ��F�W�t�ƥ�B�z��
    private void ReachUpEdgeEventHandler(object sender, EventArgs e)
    {
        // ���o�o�ͨƥ󪺪��� ���ձN sender �૬�� ballPositionHandler ����
        BallPositionHandler ballPositionHandler = sender as BallPositionHandler;

        // �p�G�૬���\ (�p�G�o�ͨƥ󪺪���O BallPositionHandler ����)
        if (ballPositionHandler != null)
        {
            // �����q�\�W�t�ƥ� �H�קK���ư��� EndGame
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
