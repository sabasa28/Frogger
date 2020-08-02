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
    public GameObject nextLvlMenu;
    public GameObject deathPanel;
    public GameObject[] hearts;
    public GameObject modeSelectionMenu;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI score;
    Player player;
    GameplayController gpController;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        gpController = FindObjectOfType<GameplayController>();
        gpController.ShowPauseMenu = ShowPauseMenu;
        gpController.ShowNextLvlMenu = ShowNextLvlMenu;
        gpController.ShowDeathPanel = ShowDeathPanel;
        gpController.HideDeathPanel = HideDeathPanel;
        gpController.UpdateHUD = UpdateTimer;
        player.UpdateHUDLives = UpdateHearts;
        player.UpdateHUDScore = UpdateScore;
    }

    void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        gpController.OnUnpause();
        pauseMenu.SetActive(false);
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

    void UpdateScore(int newScore)
    {
        score.text = "Score " + newScore;
    }

    public void StartRegularMode()
    {
        gpController.StartLevel(0);
        modeSelectionMenu.SetActive(false);
    }

    public void StartEndlessMode()
    {
        gpController.StartLevel(1);
        modeSelectionMenu.SetActive(false);
    }

    void ShowNextLvlMenu()
    {
        nextLvlMenu.SetActive(true);
    }

    public void LoadNextLevel()
    {
        gpController.ChangeLevel();
        nextLvlMenu.SetActive(false);
    }

    void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
    }

    void HideDeathPanel()
    {
        deathPanel.SetActive(false);
    }

}
