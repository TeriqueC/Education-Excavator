using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EducationExcavator
{
    public class Controller : MonoBehaviour
    {
        public void mainMenu(){
            SceneManager.LoadScene(1);
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void startGame()
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 1f;
        }

        public void viewLeaderboard()
        {
            SceneManager.LoadScene(3);
        }
    }
}