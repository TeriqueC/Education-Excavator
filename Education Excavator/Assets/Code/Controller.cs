using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void viewLeaderboard()
    {
        SceneManager.LoadScene(2);
    }

}
