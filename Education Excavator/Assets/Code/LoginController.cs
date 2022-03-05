using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EducationExcavator{
    public class LoginController : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject loginSquare;
        public GameObject signupSquare;
        public InputField loginUserName;
        public InputField loginPassword;
        public InputField signupUserName;
        public InputField signupPassword;

        public static int playerId;
        string username;
        string password;
        Hashing hashing = new Hashing();

        // Start is called before the first frame update
        void Start()
        {
            canvas.SetActive(false);  
        }

        public void loginEnter(){
            username = loginUserName.text;
            password = loginPassword.text;
            int passcode= hashing.hash(password);
            bool conformation = hashing.CheckDetails(username, passcode);
            if(conformation == true){
                playerId = hashing.retrieveId(username);
                Debug.Log("successful!!");
                Controller controller = gameObject.AddComponent<Controller>();
                controller.mainMenu();
            }
            else{
                Debug.Log("wrong but working !!");
            }
        }

        public void signinEnter(){
            username = signupUserName.text;
            password= signupPassword.text;
            bool len = hashing.checkLength(password);
            int passcode = hashing.hash(password);
            bool[] conformation = hashing.insertDetails(username, passcode, len);
            if(conformation[0] == true){
                if(conformation[1]== true){
                    Debug.Log("sign in was successful");
                    canvas.SetActive(false);
                    LeaderBoard leaderboard = new LeaderBoard();
                    leaderboard.setScores();
                }
                else{
                    Debug.Log("change password!");
                }
            }
            else{
                Debug.Log("change username");
            }
        }

        public void exit(){
            canvas.SetActive(false);
        }

        public void logIn(){
            canvas.SetActive(true);
            loginSquare.SetActive(true);
            signupSquare.SetActive(false);
        }

        public void signIn(){
            canvas.SetActive(true);
            signupSquare.SetActive(true);
            loginSquare.SetActive(false);
        }
    }
}