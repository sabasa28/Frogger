using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class UIGameplay : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject[] hearts;
    public TextMeshProUGUI timer;
    Player player;
    GameplayController gpController;
    void Start()
    {
        player = FindObjectOfType<Player>();
        gpController = FindObjectOfType<GameplayController>();
        gpController.ShowPauseMenu = ShowPauseMenu;
        gpController.HidePauseMenu = HidePauseMenu;
        gpController.UpdateHUD = UpdateTimer;
        player.UpdateHUD = UpdateHearts;
    }

    void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void UnpauseGame()
    {
        gpController.OnUnpause();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    void UpdateTimer(int newTime)
    {
        const int segsInMin = 60;
        int mins = newTime / segsInMin;
        int segs = newTime % segsInMin;
        timer.text = "Time " + string.Format("{0:00}:{1:00}",mins,segs);
    }

    void UpdateHearts(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (lives <= i && hearts[i].activeInHierarchy)
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
