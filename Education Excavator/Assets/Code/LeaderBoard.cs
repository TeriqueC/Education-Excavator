using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class LeaderBoard : MonoBehaviour
{
    string sql;
    string dbName = "URI=file:C:Education database - Copy.db";
    int player_id;
    int question_id;

    public GameObject[] names;
    public GameObject[] scores;

    void Start(){
        writeScores();
    }

    public void updateTable(){
        
    }

    public void writeScores(){
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        //sql = "UPDATE  SET question_status = "++" WHERE question_id = "+questionId+" AND player_id = '"+playerId+"'";

        Command.CommandText = sql;
        Command.ExecuteNonQuery();
        connection.Close();
    }

    public string[] readScores(){
        string[] values = new string[2];
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "SELECT ";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
        Command.CommandText = sql;
        SqliteDataReader reader = Command.ExecuteReader();
        reader.Read();
        values[0] = reader.GetString(0);//stores the question as a string in the varible question
        values[1] = reader.GetString(1);
        connection.Close();
        return values;
    }

    public string[] readSubject(){
          string[] values = new string[2];
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "SELECT ";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
        Command.CommandText = sql;
        SqliteDataReader reader = Command.ExecuteReader();
        reader.Read();
        values[0] = reader.GetString(0);//stores the question as a string in the varible question
        values[1] = reader.GetString(1);
        connection.Close();
        return values;
    }
}
