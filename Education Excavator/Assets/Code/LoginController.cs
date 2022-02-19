using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EducationExcavator{
    public class LoginController : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject loginSquare;
        public GameObject signinSquare;

        Hashing hashing = new Hashing();

        // Start is called before the first frame update
        void Start()
        {
            canvas.SetActive(false);    
        }

        public void loginEnter(){
            
        }

        public void signinEnter(){
            //string username;
            //string password;
            //string conformation = hashing.insertDetails(username, password);
            //Debug.Log(conformation);
        }

        public void logIn(){
            canvas.SetActive(true);
            signinSquare.SetActive(false);
        }

        public void signIn(){
            canvas.SetActive(true);
            loginSquare.SetActive(false);
        }
    }
}