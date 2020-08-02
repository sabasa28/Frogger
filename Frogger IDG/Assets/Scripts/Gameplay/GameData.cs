using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    public int score;
    public int time;
    public static GameData Get()
    {
        return instance;
    }
    int gameplayScore = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
