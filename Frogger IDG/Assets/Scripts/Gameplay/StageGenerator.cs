using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public int lanesToSpawn;
    GameObject nextLaneToSpawn;
    float yOffsetToSpawnLane = -0.5f;
    float yOffsetToSpawnFinish = 4.0f;
    float laneHeight = 2.0f;
    public int chanceToNotSpawnLane = 0;
    public GameObject[] posibleLanes;
    public Action PassStageCosntrains;
    public Action EndLevel;
    public FinishLine finishLine;
    enum Mode 
    {
        regular,
        endless
    }
    Mode mode;
    int[] levels = {5,10,15,20,25,30};
    int lvlToSpawn = 0;

    public void SetMode(int newMode)
    {
        if (newMode == 0) mode = Mode.regular;
        if (newMode == 1) mode = Mode.endless;
        if (mode == Mode.endless)
        {
            lanesToSpawn = 20;
        }
    }

    public int GetMode()
    {
        if (mode == Mode.regular) return 0;
        else return 1;
    }

    public void GenerateLevel()
    {
        if (mode == Mode.regular)
        {
            lanesToSpawn = levels[lvlToSpawn];
            lvlToSpawn++;
        }
        int emptyLanes = chanceToNotSpawnLane / 100 * posibleLanes.Length;
        for (int i = 0; i < lanesToSpawn; i++)
        {
            int rand = UnityEngine.Random.Range(0, posibleLanes.Length + emptyLanes);

            if (rand < posibleLanes.Length)
                nextLaneToSpawn = posibleLanes[rand];
            else
                nextLaneToSpawn = null;
            if (nextLaneToSpawn) Instantiate(nextLaneToSpawn, new Vector3(0, i * laneHeight + yOffsetToSpawnLane), Quaternion.identity);
        }
        PassStageCosntrains();
        FinishLine finish = Instantiate(finishLine, new Vector3(0, lanesToSpawn * laneHeight + yOffsetToSpawnFinish, 0), Quaternion.identity);
        finish.OnFinishLine = OnEndOfLevel;
    }

    void OnEndOfLevel()
    {
        if (mode == Mode.endless)
        {
            ReloadLevel();
        }
        if (mode == Mode.regular)
        {
            DestroyCurrentLvl();
            EndLevel();
        }
    }

    public bool AreLevelsLeft()
    {
        return (lvlToSpawn < levels.Length);
    }

    void DestroyCurrentLvl()
    {
        GameObject[] currentLevel = GameObject.FindGameObjectsWithTag("MapTile");
        for (int i = 0; i < currentLevel.Length; i++)
        {
            Destroy(currentLevel[i]);
        }
        GameObject finish = GameObject.FindGameObjectWithTag("Finish");
        Destroy(finish);
    }

    void ReloadLevel()
    {
        DestroyCurrentLvl();
        GenerateLevel();
    }
}
