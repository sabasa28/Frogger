using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public int lanesToSpawn;
    GameObject nextLaneToSpawn;
    float offsetToSpawn = -0.5f;
    public int chanceToNotSpawnLane = 0;
    public GameObject[] posibleLanes;
    public Action PassStageCosntrains;
    enum Mode 
    {
        regular,
        endless
    }
    Mode mode;

    void SetMode(int newMode)
    {
        if (newMode == 0) mode = Mode.regular;
        if (newMode == 1) mode = Mode.endless;
        if (mode == Mode.regular)
        {
            lanesToSpawn = 5;
        }
        if (mode == Mode.endless)
        {
            lanesToSpawn = 20;
        }
    }

    public void GenerateLevel(int newMode)
    {
        SetMode(newMode);
        int emptyLanes = chanceToNotSpawnLane / 100 * posibleLanes.Length;
        for (int i = 0; i < lanesToSpawn; i++)
        {
            int rand = UnityEngine.Random.Range(0, posibleLanes.Length + emptyLanes);

            if (rand < posibleLanes.Length)
                nextLaneToSpawn = posibleLanes[rand];
            else
                nextLaneToSpawn = null;
            if (nextLaneToSpawn) Instantiate(nextLaneToSpawn, new Vector3(0, i*2 + offsetToSpawn), Quaternion.identity);
        }
        PassStageCosntrains();
    }
}
