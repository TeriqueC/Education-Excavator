using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;

public class Login
{
    string dbName = "Data source= PasswordTest.db";
    string sql;

    int hashValue = 0;
    bool size;
    string userName;
    string password;
    int hashedValue;

    public int hash(string word)
    {
        int length = word.Length;
        for(int i = 0; i < length; i = i+1)
        {
            char wordChar = word[i];
            int charValue = Convert.ToInt32(wordChar);
            hashValue = hashValue + charValue;
        }

        int finalValue = hashValue % 11;
        return finalValue;
    }

    public bool checkLength(string word)
    {            
        int length = word.Length;
        if (length > 8){
            Console.WriteLine("word is too long");
            size = false;
            return size;
        }
        Console.WriteLine("hashing password");
        size = true;
        return size;
    }

    static void Main(string[] args)
    {
        Login login = new Login();
        Console.WriteLine("Type in a username: ");
        login.userName = Console.ReadLine();
        Console.WriteLine("Type in a word to be hashed: ");
        login.password = Console.ReadLine();
        bool len = login.checkLength(login.password);
        if(len == true)
        {
            login.hashedValue = login.hash(login.password);
            Console.WriteLine(login.hashedValue);
            login.insertDetails();
        }
        login.printDetails();
    }
        
    public void insertDetails()
    {
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "INSERT INTO Passwords(user_name, password) VALUES('" + userName+ "', '" + hashedValue + "')";

        Command.CommandText = sql;
        Command.ExecuteNonQuery();

        connection.Close();
    }

    public void printDetails()
    {
        SqliteConnection connection = new SqliteConnection(dbName);
        connection.Open();
        SqliteCommand Command = connection.CreateCommand();

        sql = "SELECT * FROM Passwords";

        Command.CommandText = sql;
        SqliteDataReader reader = Command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int passcode = reader.GetInt32(2);
            Console.WriteLine(id + " " + name + " " + passcode);
        }
        connection.Close();
    }
}
