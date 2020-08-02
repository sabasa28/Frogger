using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    public int score;
    public int time;
    public int mode;
    public bool win;
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

    public void SetData(int newScore, int newTime, int newMode, bool newWin)
    {
        score = newScore;
        time = newTime;
        mode = newMode;
        win = newWin;
    }
}
