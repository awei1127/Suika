using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // �}�l�C��
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
        GameDirector.Instance.UpdateGameState(GameState.InGame);
    }
    // �}�ҼȰ����O
    public void GamePause()
    {
        Time.timeScale = 0f;
        MenuView.Instance.PausePanel.SetActive(true);
        GameDirector.Instance.UpdateGameState(GameState.Paused);
    }

    // �^��C��
    public void GameResume()
    {
        Time.timeScale = 1f;
        MenuView.Instance.PausePanel.SetActive(false);
        GameDirector.Instance.UpdateGameState(GameState.InGame);
    }

    // �}�ҳ]�w���O
    public void ShowOptionPanel()
    {
        MenuView.Instance.OptionPanel.SetActive(true);
    }

    // �����]�w���O (�x�s���q�]�w)
    public void HideOptionPanel()
    {
        MenuView.Instance.OptionPanel.SetActive(false);
    }

    // �}�Ҧ^��D��檺�T�{���O
    public void ShowConfirmPanel()
    {
        MenuView.Instance.ConfirmPanel.SetActive(true);
    }

    // ���æ^��D��檺�T�{���O (���a�b�T�{�����U�_)
    public void HideConfirmPanel()
    {
        MenuView.Instance.ConfirmPanel.SetActive(false);
    }

    // �^��D��� (���a�b�T�{�����U�O)
    public void GoMainMenu()
    {
        GameDirector.Instance.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    // ���s�}�l�C��
    public void RestartGame()
    {
        GameDirector.Instance.UpdateGameState(GameState.InGame);
        SceneManager.LoadScene("GameScene");
    }
}
