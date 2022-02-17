using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void setQuestion(){
        
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void viewLeaderboard()
    {
        SceneManager.LoadScene(2);
    }

}
