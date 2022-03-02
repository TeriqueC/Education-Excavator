using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

namespace EducationExcavator{
    public class LeaderBoard : MonoBehaviour
    {
        string sql;
        string dbName = "URI=file:C:Education database - Copy.db";
        int subject_id;

        public GameObject[] names;
        public GameObject[] scores;

        void Start(){
        }

        public int setId(int i){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_id FROM Scores WHERE score_id ="+i+"";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int player_id = reader.GetInt32(0);//stores the question as a string in the varible question
            connection.Close();
            return player_id;
        }

        public void updateTable(){
            for(int i = 0; i < scores.Length; i++){

            }
        }

        public void writeScores(int i){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "INSERT INTO Scores(player_id, player_score, subject_id) VALUES("+Controller.playerId+", "+GameController.score+", "+Controller.subjectId+")";

            Command.CommandText = sql;
            Command.ExecuteNonQuery();
            connection.Close();
        }

        public void updateScores(){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "UPDATE Scores SET player_score = "+GameController.score+" WHERE player_id = "+Controller.playerId+"";

            Command.CommandText = sql;
            Command.ExecuteNonQuery();
            connection.Close();
        }

        public int readScores(int i){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_score FROM Scores WHERE score_id ="+i+"";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int score = reader.GetInt32(0);//stores the question as a string in the varible question
            connection.Close();
            return score;
        }

        public string readNames(int i){
            int player_id = setId(i);
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_name FROM Users WHERE player_id ="+player_id+"";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            string name = reader.GetString(0);//stores the question as a string in the varible question
            connection.Close();
            return name;
        }

        /*public int readSubject(int i, int subject_id){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_score FROM Users WHERE score_id = "+i+" AND subject_id = "+subject_id+"";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int scores = reader.GetInt32(0);
            connection.Close();
            return scores;
        }*/
    }
}