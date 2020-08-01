using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public int lanesToSpawn;
    GameObject laneToSpawn;
    float offsetToSpawn = -0.5f;
    public int chanceToNotSpawnLane = 0;
    public GameObject[] posibleLanes;
    public Action PassStageCosntrains;
    private void Start()
    {
        int emptyLanes = chanceToNotSpawnLane / 100 * posibleLanes.Length;
        for (int i = 0; i < lanesToSpawn; i++)
        {
            int rand = UnityEngine.Random.Range(0, posibleLanes.Length + emptyLanes);

            if (rand < posibleLanes.Length)
                laneToSpawn = posibleLanes[rand];
            else
                laneToSpawn = null;
            if (laneToSpawn) Instantiate(laneToSpawn, new Vector3(0, i*2 + offsetToSpawn), Quaternion.identity);
        }
        PassStageCosntrains();
    }
}
