using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public int lanesToSpawn;
    GameObject laneToSpawn;
    float offsetToSpawn = -0.5f;
    public GameObject[] posibleLanes;
    private void Start()
    {
        for (int i = 0; i < lanesToSpawn; i++)
        {
            int rand = Random.Range(0, 12);

            if (rand < posibleLanes.Length)
                laneToSpawn = posibleLanes[rand];
            else
                laneToSpawn = null;
            if (laneToSpawn) Instantiate(laneToSpawn, new Vector3(0, i*2 + offsetToSpawn), Quaternion.identity);
        }
    }
}
