using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class QuestionGenerator
{
    string dbName= "URI=file: Education database.db";
    public string getQuestion(){
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        Command.CommandText = "SELECT question FROM Questions WHERE question_id = 1";
        SqliteDataReader reader = Command.ExecuteReader();
        reader.Read();

        string question = reader.GetString(1); 

        connection.Close();
        return question;
    }
}