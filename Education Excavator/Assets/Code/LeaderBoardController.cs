using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EducationExcavator{
    public class LeaderBoardController : MonoBehaviour
    {
        public GameObject[] names;
        public GameObject[] scores;
        public GameObject printScore;

        LeaderBoard leaderBoard = new LeaderBoard();

        void Start(){
            printScore.gameObject.GetComponent<Text>().text = "Well Done!!! your score is: "+ GameController.score;
            updateTable();
        }

          public void updateTable(){
            int[] playerScores = leaderBoard.readScores();
            string[] playerNames = leaderBoard.readNames();
            for(int i = 0; i < scores.Length; i++){
                scores[i].GetComponent<Text>().text= ""+playerScores[i]+"";
                names[i].GetComponent<Text>().text= playerNames[i];
            }
        }
    }
}