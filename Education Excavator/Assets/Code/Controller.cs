using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Player player;
    QuestionGenerator generator = new QuestionGenerator();
    string dbfile;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
        
        //generator.createTable();
        setQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        player.movement();
        player.shoot();
    }

    public void setQuestion(){
        string question = generator.getQuestion();
        Debug.Log(question);
    }

    public void pause()
    {
        
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
