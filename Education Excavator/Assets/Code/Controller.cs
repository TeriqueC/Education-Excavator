using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EducationExcavator
{
    public class Controller : MonoBehaviour
    {
        public static int playerId;
        public static int subjectId;
        public static bool gameOver;

        public void mainMenu(){
            SceneManager.LoadScene(1);
            gameOver = false;
            playerId= LoginController.playerId;
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void startGame(int subject)
        {
            subjectId = subject;
            SceneManager.LoadScene(2);
            Time.timeScale = 1f;
        }

        public void viewLeaderboard()
        {
            SceneManager.LoadScene(3);
        }
    }
}