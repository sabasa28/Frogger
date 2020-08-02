using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndcreen : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI modeText;
    bool win = false;
    int score = 0;
    int time = 0;
    int mode = 0;
    private void Start()
    {
        win = GameData.Get().win;
        score = GameData.Get().score;
        time = GameData.Get().time;
        mode = GameData.Get().mode;

        if (win) titleText.text = "Congratulations! You won";
        else titleText.text = "Game over";

        scoreText.text = "Score : " + score;

        const int segsInMin = 60;
        int mins = time / segsInMin;
        int segs = time % segsInMin;
        timerText.text = "Time : " + string.Format("{0:00}:{1:00}", mins, segs);
        if (mode == 0) modeText.text = "Mode : Regular";
        else modeText.text = "Mode : Endless";
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameplay()
    {
        SceneManager.LoadScene(2);
    }
}
