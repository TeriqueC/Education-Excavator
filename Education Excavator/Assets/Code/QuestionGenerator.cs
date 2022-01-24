using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class QuestionGenerator
{
    string dbName = "URI=file:C:Education database.db";
    string sql;
    
    public void createTable(){
         SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "CREATE TABLE IF NOT EXISTS Test (test_id INT)";

        Command.CommandText = sql;
        Command.ExecuteNonQuery();

        sql= "INSERT INTO Test (test_id) values ('1')";

        Command.CommandText= sql;
        Command.ExecuteNonQuery();
    }
    
    public string getQuestion(){
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "SELECT question FROM Question WHERE question_id=1";

        Command.CommandText = sql;
        SqliteDataReader reader = Command.ExecuteReader();
        reader.Read();

        string question = reader.GetString(0); 

        connection.Close();
        return question;
    }
}