using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EducationExcavator{
    public class LoginController : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject loginSquare;
        public GameObject signinSquare;
        public InputField userNameInput;
        public InputField passwordInput;

        static int playerId;
        string username;
        string password;
        Hashing hashing = new Hashing();

        // Start is called before the first frame update
        void Start()
        {
            canvas.SetActive(false);    
        }

        public void loginEnter(){
            username = userNameInput.text;
            password = passwordInput.text;
            bool conformation = hashing.CheckDetails(username, password);
            if(conformation == true){
                playerId = hashing.retrieveId(username);
                Debug.Log("successful!!");
                Controller controller = gameObject.AddComponent<Controller>();
                controller.mainMenu(playerId);
            }
            else{
                Debug.Log("wrong but working !!");
            }
        }

        public void signinEnter(){
            username = userNameInput.text;
            password= passwordInput.text;
            bool[] conformation = hashing.insertDetails(username, password);
            if(conformation[0] == true){
                if(conformation[1]== true){
                    Debug.Log("sign in was successful");
                    canvas.SetActive(false);
                }
                else{
                    Debug.Log("change password!");
                }
            }
            else{
                Debug.Log("change username");
            }
        }

        public void logIn(){
            canvas.SetActive(true);
            loginSquare.SetActive(true);
            signinSquare.SetActive(false);
        }

        public void signIn(){
            canvas.SetActive(true);
            signinSquare.SetActive(true);
            loginSquare.SetActive(false);
        }
    }
}