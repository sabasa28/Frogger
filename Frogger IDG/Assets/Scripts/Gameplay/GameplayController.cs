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
    float timeInLvlF = 0;
    int timeInLvlI = 0;

    private void Awake()
    {
        FindObjectOfType<StageGenerator>().PassStageCosntrains = PassStageConstrains;
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
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
            ConstrainedObjs[i].minX = -15;
            ConstrainedObjs[i].maxX = 25;
        }
    }
}
