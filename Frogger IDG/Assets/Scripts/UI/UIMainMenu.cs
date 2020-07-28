using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject credits;
    public void LoadGameplay()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    public void ShowCredits()
    {
        credits.SetActive(true);
    }
    public void HideCredits()
    { 
        credits.SetActive(false);
    }
}
