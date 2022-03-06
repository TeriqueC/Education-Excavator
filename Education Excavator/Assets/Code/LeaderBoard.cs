using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

namespace EducationExcavator{
    public class LeaderBoard
    {
        string sql;
        string dbName = "URI=file:C:Education database - Copy.db";
        static int[] subject_ids = new int[5];
        static int[] player_ids = new int[5];

        public void setScores(){
            for(int i =1; i < 5; i++){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();

                sql = "INSERT INTO Scores(player_id, player_score, subject_id) VALUES("+LoginController.playerId+", "+0+", "+i+")";
                Command.CommandText = sql;
                Command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void updateScore(){
            int currentHighScore = currentScore();
            if(GameController.score > currentHighScore){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();

                sql = "UPDATE Scores SET player_score = "+GameController.score+" WHERE player_id = "+Controller.playerId+" AND subject_id ="+Controller.subjectId+"";

                Command.CommandText = sql;
                Command.ExecuteNonQuery();
                connection.Close();
                Debug.Log("update score working");
            }
        }

        public int currentScore(){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_score FROM Scores WHERE player_id ="+Controller.playerId+"";
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int currentHighScore = reader.GetInt32(0);
            connection.Close();
            return currentHighScore;
        }

        public string[] readSubject(){
            string[] subjects = new string[5];
            for(int i=0; i< subjects.Length; i++){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();
                sql = "SELECT subject FROM Users WHERE subject_id = "+subject_ids[i]+"";
                Command.CommandText = sql;
                SqliteDataReader reader = Command.ExecuteReader();
                reader.Read();
                subjects[i] = reader.GetString(0);
                connection.Close();
            }
            return subjects;
        }

        public int[] readScores(){
            int[] scores = new int[5];
            for(int i = 0; i < scores.Length; i++){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();
                sql = "SELECT player_score, player_id, subject_id FROM Scores ORDER BY player_score DESC";
                Command.CommandText = sql;
                SqliteDataReader reader = Command.ExecuteReader();
                reader.Read();
                scores[i] = reader.GetInt32(0);
                player_ids[i] = reader.GetInt32(1);
                subject_ids[i] = reader.GetInt32(2);
                connection.Close();
            }
            return scores;
        }

        public string[] readNames(){
            string[] names = new string[5];
            for(int i=0; i< names.Length; i++){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();
                sql = "SELECT player_name FROM Users WHERE player_id = "+player_ids[i]+"";
                Command.CommandText = sql;
                SqliteDataReader reader = Command.ExecuteReader();
                reader.Read();
                names[i] = reader.GetString(0);
                connection.Close();
            }
            return names;
        }

        public int[] readSubjectScores(){
            int[] scores = new int[5];
            for(int i = 0; i < scores.Length; i++){
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();
                sql = "SELECT player_score, player_id FROM Scores WHERE subject_id = "+Controller.subjectId+"";
                Command.CommandText = sql;
                SqliteDataReader reader = Command.ExecuteReader();
                reader.Read();
                scores[i] = reader.GetInt32(0);
                player_ids[i] = reader.GetInt32(1);
                connection.Close();
            }
            return scores;
        }
    }
}