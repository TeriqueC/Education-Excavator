using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EducationExcavator{
    public class LeaderBoardController : MonoBehaviour
    {
        public GameObject[] names;
        public GameObject[] scores;
        public GameObject[] subjects;
        public GameObject printScore;

        LeaderBoard leaderBoard = new LeaderBoard();

        void Start(){
            printScore.gameObject.GetComponent<Text>().text = "Your current score is: "+ GameController.score;
            updateTable();
        }

          public void updateGameover(){
            int[] playerScores = leaderBoard.readSubjectScores();
            string[] playerNames = leaderBoard.readNames();
            for(int i = 0; i < scores.Length; i++){
                scores[i].GetComponent<Text>().text= ""+playerScores[i]+"";
                names[i].GetComponent<Text>().text= playerNames[i];
            }
          }

        public void updateTable(){
        int[] playerScores = leaderBoard.readScores();
        string[] playerNames = leaderBoard.readNames();
        string[] scoreSubject = leaderBoard.readSubject();
            for(int i = 0; i < scores.Length; i++){
                scores[i].GetComponent<Text>().text= ""+playerScores[i]+"";
                names[i].GetComponent<Text>().text= playerNames[i];
                subjects[i].GetComponent<Text>().text = scoreSubject[i];
            }
        }
    }
}