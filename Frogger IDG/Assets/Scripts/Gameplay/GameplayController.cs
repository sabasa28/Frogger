using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    bool pause = false;
    Player player;
    StageGenerator stageGenerator;
    public Action ShowPauseMenu;
    public Action ShowNextLvlMenu;
    public Action ShowDeathPanel;
    public Action HideDeathPanel;
    public Action<int> UpdateHUD;
    float minXForScrollingObjs = -15;
    float maxXForScrollingObjs = 25;
    float minXForPlayer = -2.5f;
    float maxXForPlayer = 11.5f;
    float minYForPlayer = -2.0f;
    float timeInLvlF = 0;
    int timeInLvlI = 0;

    private void Awake()
    {
        Time.timeScale = 0;
        stageGenerator = FindObjectOfType<StageGenerator>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        stageGenerator.PassStageCosntrains = PassStageConstrains;
        stageGenerator.EndLevel = OnLevelEnded;
        PassPlayerConstrains();
        player.PauseGame = OnPause;
        player.OnDeath = OnPlayersDeath;
        player.OnRespawn = OnPlayersRespawn;
        player.ableToMove = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    void OnPause()
    {
        Time.timeScale = 0.0f;
        pause = true;
        ShowPauseMenu();
        player.ableToMove = false;
    }

    public void OnUnpause()
    {
        Time.timeScale = 1.0f;
        pause = false;
        player.ableToMove = true;
    }

    void UpdateTimer()
    {
        timeInLvlF += Time.deltaTime;
        if ((int)timeInLvlF != timeInLvlI)
        {
            timeInLvlI = (int)timeInLvlF;
            UpdateHUD(timeInLvlI);
        }
    }

    void PassStageConstrains()
    {
        ScrollingObj[] ConstrainedObjs = FindObjectsOfType<ScrollingObj>();
        for (int i = 0; i < ConstrainedObjs.Length; i++)
        {
            ConstrainedObjs[i].minX = minXForScrollingObjs;
            ConstrainedObjs[i].maxX = maxXForScrollingObjs;
        }
    }

    void PassPlayerConstrains()
    {
        player.minX = minXForPlayer;
        player.maxX = maxXForPlayer;
        player.minY = minYForPlayer;
    }

    public void StartLevel(int mode)
    {
        stageGenerator.SetMode(mode);
        stageGenerator.GenerateLevel();
        player.ableToMove = true;
        Time.timeScale = 1;
    }

    public void OnLevelEnded()
    {
        Time.timeScale = 0;
        ShowNextLvlMenu();
    }
    public void ChangeLevel()
    {
        if (stageGenerator.AreLevelsLeft())
        {
            Time.timeScale = 1;
            stageGenerator.GenerateLevel();
        }
        else
        {
            GameData.Get().score = player.GetFullScore();
            GameData.Get().time = timeInLvlI;
            SceneManager.LoadScene(3);
        }
    }

    void OnPlayersDeath(int lives)
    {
        player.ableToMove = false;
        Time.timeScale = 0;
        if (lives > 0) ShowDeathPanel();
        else
        {
            GameData.Get().score = player.GetFullScore();
            GameData.Get().time = timeInLvlI;
            SceneManager.LoadScene(4);
        }
    }

    void OnPlayersRespawn()
    {
        player.ableToMove = true;
        Time.timeScale = 1;
        HideDeathPanel();
    }
}
