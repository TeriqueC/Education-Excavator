using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
 public Player player;
 public GameObject canvas;
  public GameObject PauseMenu;
 public GameObject questionPopup;
   public static bool isGamePaused = false;
    // Start is called before the first frame update
    void Start()
    {

        canvas.SetActive(false);
        player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        player.movement();
        player.shoot();
    }

     public void pause()
    {
        canvas.SetActive(true);
        questionPopup.SetActive(false);
        //PauseMenu.SetActive(true);
        Time.timeScale= 0f;
        isGamePaused = true;
    }

    public void resume(){
        canvas.SetActive(false);
        Time.timeScale= 1f;
        isGamePaused = false;
    }
}
