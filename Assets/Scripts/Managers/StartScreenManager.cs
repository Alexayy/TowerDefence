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
        StartCoroutine(IStartGame());
    }

    public void QuitAndExitApplication()
    {
        Application.Quit();
    }

    private IEnumerator IStartGame()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
    }
}
