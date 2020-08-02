using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndcreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        scoreText.text = "Score : " + GameData.Get().score;
        int finalTime = GameData.Get().time;
        const int segsInMin = 60;
        int mins = finalTime / segsInMin;
        int segs = finalTime % segsInMin;
        timerText.text = "Time : " + string.Format("{0:00}:{1:00}", mins, segs);
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
