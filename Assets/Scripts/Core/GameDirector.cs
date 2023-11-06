using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;
    public GameState CurrentGameState { get; private set; }

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
    }

    public void UpdateGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState)
        {
            return;
        }

        CurrentGameState = newGameState;

    }

    private void GameOver()
    {
        UpdateGameState(GameState.GameOver);
    }

}
