using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // 開始遊戲
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
        GameDirector.Instance.UpdateGameState(GameState.InGame);
    }
    // 開啟暫停面板
    public void GamePause()
    {
        Time.timeScale = 0f;
        MenuView.Instance.PausePanel.SetActive(true);
        GameDirector.Instance.UpdateGameState(GameState.Paused);
    }

    // 回到遊戲
    public void GameResume()
    {
        Time.timeScale = 1f;
        MenuView.Instance.PausePanel.SetActive(false);
        GameDirector.Instance.UpdateGameState(GameState.InGame);
    }

    // 開啟設定面板
    public void ShowOptionPanel()
    {
        MenuView.Instance.OptionPanel.SetActive(true);
    }

    // 關閉設定面板 (儲存音量設定)
    public void HideOptionPanel()
    {
        MenuView.Instance.OptionPanel.SetActive(false);
    }

    // 開啟回到主選單的確認面板
    public void ShowConfirmPanel()
    {
        MenuView.Instance.ConfirmPanel.SetActive(true);
    }

    // 隱藏回到主選單的確認面板 (當玩家在確認中按下否)
    public void HideConfirmPanel()
    {
        MenuView.Instance.ConfirmPanel.SetActive(false);
    }

    // 回到主選單 (當玩家在確認中按下是)
    public void GoMainMenu()
    {
        GameDirector.Instance.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    // 重新開始遊戲
    public void RestartGame()
    {
        GameDirector.Instance.UpdateGameState(GameState.InGame);
        SceneManager.LoadScene("GameScene");
    }
}
