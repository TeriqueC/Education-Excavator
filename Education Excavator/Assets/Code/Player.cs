using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EducationExcavator
{
       public class Player : MonoBehaviour
        //MonoBehaviour allows the unity engine to help with instantiating and calling different methods within the class
        //also allows interactions with components attached to GameObjects
    {
        private float player_speed=10;
        public static int player_health=5;
        private float player_xpos;
        private float player_ypos;

        private new Rigidbody2D rigidbody;
        //rigidbody allows physics to be aplied to gameobjects
        public GameObject projectile;

        public void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            player_health = 3;
            //gets the component rigid body which applies physics to an object, attached to the gameobject
        }// the Awake method is called before the first frame update 

        public void movement()
        {
            Vector2 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x,-9.4f,9.4f); // math.clamp allows me to restrict the players movement on the x axis
            pos.y = Mathf.Clamp(pos.y,-4.5f,-0.5f); // math.clamp allows me to restrict the players movement on the y axis
            transform.position = pos;

            player_xpos = Input.GetAxisRaw("Horizontal");
            //Horizonal allows input from A key,D key, left arrow and right arrow
            player_ypos = Input.GetAxisRaw("Vertical");
            //Vertical allow input from W key, s key, Up arrow and down arrow
            rigidbody.velocity = new Vector2(player_xpos, player_ypos) * player_speed;
        }//vector2 are coordinate points on the x and y axis

        public void shoot()
        {
            if (Input.GetButtonDown("Fire2"))//checks input to be the space key
            {
                Vector2 initialPos = transform.position; //players position is stored as vector2 called initialPos
                initialPos.y = initialPos.y + 0.5f; // sets initialPos to be a bit higher then the players location
                Instantiate(this.projectile, initialPos, transform.rotation);//instantiates projectile
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            player_health = player_health - 1;
            string lives = player_health.ToString();
            
            if (player_health == 0)
            {
                Debug.Log("game over");
            }
        }//this method checks for collisions that take place in the game
    }
}