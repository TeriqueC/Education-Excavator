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

        public void mainMenu(int Id){
            SceneManager.LoadScene(1);
            playerId= Id;
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void startGame(int subject)
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 1f;
            subjectId = subject;
        }

        public void viewLeaderboard()
        {
            SceneManager.LoadScene(3);
        }
    }
}