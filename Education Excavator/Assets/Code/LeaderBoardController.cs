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
            if(Player.player_health == 0){
                updateGameover();
            }
            else{
                updateTable();
            }
        }

          public void updateGameover(){
            int[] playerScores = leaderBoard.readSubjectScores();
            for(int i = 0; i < scores.Length; i++){
                scores[i].GetComponent<Text>().text= ""+playerScores[i]+"";
                names[i].GetComponent<Text>().text= leaderBoard.readNames(i);
            }
          }

        public void updateTable(){
        int[] playerScores = leaderBoard.readScores();
            for(int i = 0; i < scores.Length; i++){
                scores[i].GetComponent<Text>().text= ""+playerScores[i]+"";
                names[i].GetComponent<Text>().text= leaderBoard.readNames(i);
                subjects[i].GetComponent<Text>().text = leaderBoard.readSubject(i);
            }
        }
    }
}