using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace EducationExcavator{

    public class GameController : MonoBehaviour
    {
    public Player player;
    public Asteroid asteroid;
    public GameObject canvas;
    public GameObject PauseMenu;
    public GameObject questionPopup;
    public GameObject questionBox;
    public GameObject health;
    public GameObject scoreBox;
    public GameObject[] answerBoxes;
    public GameObject[] asteroids;

    QuestionGenerator generator = new QuestionGenerator();
    int playerId;
    string Answer;
    int score= 0;

    public static bool isGamePaused = false;
        // Start is called before the first frame update
        void Start()
        {
            generator.generateQuestions(1);
            updateQuestion();
            updateAnswers();
            canvas.SetActive(false);
            player.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            player.movement();
            player.shoot();
            checkHealth();
            bool change = asteroid.checkQuestion();
            if(change == true){
                checkAnswer();
                updateQuestion();
                updateAnswers();
            }
        }

        public void checkHealth(){
            int life = player.checkLives();
            string lives = life.ToString();
            health.GetComponent<Text>().text= "Health:  "+lives;
            if(life == 0){
                SceneManager.LoadScene(4);
            }
        }

        public void checkAnswer(){ 
            string word = answerBoxes[1].GetComponent<Text>().text;
            string answer= word.Substring(4);
            bool correct = generator.checkAnswer(answer);
            if(correct == true){
                score= score+10;
                scoreBox.GetComponent<Text>().text="Score:  "+score;
            }
            score = score-5;
            scoreBox.GetComponent<Text>().text="Score:  "+score;
        }

        public void pause()
        {
            canvas.SetActive(true);
            questionPopup.SetActive(false);
            Time.timeScale= 0f;
            isGamePaused = true;
        }

        public void resume(){
            canvas.SetActive(false);
            Time.timeScale= 1f;
            isGamePaused = false;
        }

        public void updateQuestion(){
            string[] newQuestion = generator.updateQuestion();
            questionBox.GetComponent<Text>().text= newQuestion[0];
            Answer = newQuestion[1];
        }

        public void updateAnswers(){
            string[] newAnswers = generator.updateAnswers();
            for(int i =0; i < answerBoxes.Length; i++){
                int j = i+1;
                answerBoxes[i].GetComponent<Text>().text= j+")  "+newAnswers[i];
            }
        }
    }
}