using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public GameObject TitlePanel;
    public GameObject OptionPanel;
    public GameObject PausePanel;
    public GameObject ConfirmPanel;
    public GameObject GameOverPanel;

    public static MenuView Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
