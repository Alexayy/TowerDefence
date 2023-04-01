using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _quitGame;

    public void StartAndMoveToNextScene()
    {
        SceneManager.LoadScene("Game");
        GameManager.Instance.StartGame();
    }

    public void QuitAndExitApplication()
    {
        Application.Quit();
    }
}
