using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    bool pause = false;
    Player player;
    StageGenerator stageGenerator;
    public Action HidePauseMenu;
    public Action ShowPauseMenu;
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
        FindObjectOfType<StageGenerator>().PassStageCosntrains = PassStageConstrains;
        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        PassPlayerConstrains();
        player.SwitchPauseState = SwitchPauseState;
    }

    private void Update()
    {
        UpdateTimer();
    }

    void SwitchPauseState()
    {
        if (pause) OnUnpause();
        else OnPause();
    }

    void OnPause()
    {
        Time.timeScale = 0.0f;
        pause = true;
        ShowPauseMenu();
    }

    public void OnUnpause()
    {
        Time.timeScale = 1.0f;
        pause = false;
        HidePauseMenu();
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

}
