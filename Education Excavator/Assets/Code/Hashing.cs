using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;

namespace EducationExcavator
{
    class Hashing
    {
        string dbName = "URI=file:C:Education database.db";
        string sql;

        int hashValue = 0;
        int hashedValue;
        static int playerId;
        public int counter;

        public int hash(string word)
        {
            int length = word.Length;
            hashValue= 0;
            for(int i = 0; i < length; i = i+1)
            {
                char wordChar = word[i];
                int charValue = Convert.ToInt32(wordChar);
                hashValue = hashValue + charValue;
            }

            int finalValue = hashValue % 51;
            return finalValue;
        }

        public bool checkLength(string word)
        {
            bool size;
            int length = word.Length;
            if (length < 6){
                size = false;
                return size;
            }
           size = true;
           return size;
        }

        public bool[] insertDetails(string userName, int password, bool len)
        {
            bool userCollision = usernameCollision(userName);
            bool[] confirm = new bool[2];
            if(len == true && userCollision==false)
            {
                bool collision = passwordCollision(password);
                if (collision == false)
                {
                    SqliteConnection connection = new SqliteConnection(dbName);
                    connection.Open();
                    SqliteCommand Command = connection.CreateCommand();

                    sql = "INSERT INTO Users(player_name, hashed_password) VALUES('" + userName + "', '" + password + "')";

                    Command.CommandText = sql;
                    Command.ExecuteNonQuery();
                    connection.Close();
                    QuestionGenerator generator = new QuestionGenerator();
                    generator.setNewStatus(retrieveId(userName));
                    confirm[0] = true;
                    confirm[1] = true;
                    return confirm;
                }
                confirm[0]= true;
                confirm[1] = false;
                return confirm;
            }
            confirm[0]= false;
            confirm[1] = false;
            return confirm;         
        }

        public bool CheckDetails(string userName, int password)
        {
           if (usernameCollision(userName) == true)
           {
                SqliteConnection connection = new SqliteConnection(dbName);
                connection.Open();
                SqliteCommand Command = connection.CreateCommand();

                sql = "SELECT hashed_password FROM Users WHERE player_name ='"+userName+"'";

                Command.CommandText = sql;
                SqliteDataReader reader = Command.ExecuteReader();
                reader.Read();
                int currentPassword = reader.GetInt32(0);
                connection.Close();
                if(currentPassword == password)
                {
                    retrieveId(userName);
                    return true;
                }
                return false;
           }
           return false;
        }
        
         public int retrieveId(string username)
        {
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT player_id FROM Users WHERE player_name ='" + username + "'";

            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            playerId = reader.GetInt32(0);
            connection.Close();
            return playerId;
        }

         public bool usernameCollision(string newUsername)
        {
           SqliteConnection connection = new SqliteConnection(dbName);
           connection.Open();
           SqliteCommand Command = connection.CreateCommand();

           sql = "SELECT user_id FROM passwords WHERE user_name =" + newUsername + "";

           Command.CommandText = sql;
           SqliteDataReader reader = Command.ExecuteReader();
           reader.Read();
           if (reader.GetValue(0) == null)
           {
                connection.Close();
                return false;
            }
           connection.Close();
           return true;
        }

        public bool passwordCollision(int newPassword)
        {
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT user_id FROM passwords WHERE user_name =" + newPassword + "";

            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            if (reader.GetValue(0) == null)
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
    }
}
