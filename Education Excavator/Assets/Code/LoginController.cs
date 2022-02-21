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

        int playerId;
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
            Debug.Log(username);
            password = passwordInput.text;
            Debug.Log(password);
            bool conformation = hashing.CheckDetails(username, password);
            if(conformation == true){
                playerId = hashing.retrieveId(username);
                Debug.Log("working!!");
            }
            else{
                Debug.Log("wrong but working !!");
            }
        }

        public void signinEnter(){
            //string username;
            //string password;
            //string conformation = hashing.insertDetails(username, password);
            //Debug.Log(conformation);
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