using UnityEngine;
using Mono.Data.Sqlite;

namespace EducationExcavator
{
    public class Node
    {
        public int[] data = new int[2];
        //this is the data that is being stored into the nodes of the linked list
        public Node next;
        //this is an instance of the class named next
        public QuestionGenerator generator = new QuestionGenerator();

        public Node(int i)// this is the constructor for the node class
        {
            data[0] = i;
            // i is a parameter to the constructor method and its value is set to the data variable, i is also the question id in my database
            data[1] = generator.setData(i);
            // i is used as a parameter in my generator class to set the second value in my array to equal the question status
            next = null;
            // the instance of the class called next is now set to null
        }

        public void addSort(int data)//this method takes in data as a parameter to add to the list, data is sorted in order
        {
            int state = generator.setData(data);
            if (next == null)//if next is equal to null then there are no more nodes at the end of the list
            {
                next = new Node(data);// as list is empty, next will be set to a new node with the data value assigned to it
            }
            else if (state >= next.data[1])//if next is not null this part of the code will be executed if data's value is less then the current nodes data
            {
                Node temp = new Node(data);// a temporary node is used to store the data
                temp.next = this.next;//this line sets the node next after temp to equal the value of the node straight after the current node(the node the method was called upon)
                this.next = temp;// the next node after the current node is set to equal the temporary node,
                                 // this switches the position of the data in current node with temp as the list needs to be sorted in smallest to largest order 
            }
            else
            {
                next.addSort(data);//uses the same method on the next node (use of recursion) so that the search continues down the list,
                                   //so next.next would be searched and so on until the list is sorted
            }
        }
    }

    public class LinkedList
    {
        public Node start;// creates and instance of the node class called start
        public LinkedList()
        {
            start = null;//constructor method wich sets the start (head node) to null
        }

        public void addSort(int data)// this adds a node in a sorted order
        {
            QuestionGenerator generator = new QuestionGenerator();
            int state = generator.setData(data);
            if (start == null)// if start is equal to null the list is empty
            {
                start = new Node(data);// new start node is created which stores data
            }
            else if (state >= start.data[1])// checks is the start nodes data is greater then the data being sorted
            {
                Node temp = new Node(data);// if the start nodes data is greater then the data sorted then a temp node is created
                temp.next = this.start;// the next node after temp will now equal the current node in the list
                this.start = temp;//the current node in the linked list will now have the same values as the temporary node
                // this will switch the position between the current node and the new node as the new node needs to be in sorted order
            }
            else
            {
                start.addSort(data);// if the data of the new node is greater than the start nodes data then the node is in the wrong position
                                    //and will need to be inserted in elsewhere, therefore the sort method from the node class is called to put the node in the right position
            }
        }

        public string[] removeStart()//this method removes the first item on the list
        {
            if (start == null)//if start is null then the list is empty
            {
                return null;
            }
            else
            {
                string[] question = new string[2];
                QuestionGenerator generator = new QuestionGenerator();//object of my question generation class
                question[0] = generator.getQuestion(start.data[0]);//uses the int in the array[0] (primary key for db) in the print question method, and stores results in a string called question
                question[1] = generator.getAnswer(start.data[0]);
                start = start.next; // if the list is not empty then the next node is set to replace the previous starting node of the list
                return question;//returns the string question
            }
        }
    }

    public class QuestionGenerator
    {
        string dbName = "URI=file:C:Education database - Copy.db";//the location at which the database is stored
        string sql;//string which will be used later to store sql queries
        public static int questionId;
        static string[] currentQuestion = new string[2];

        public LinkedList list = new LinkedList();//instance of my Linked list class

        public int setData(int data)//sets the second value of the array in the linked list node(uses the question status to prioritise position
        {
            SqliteConnection connection = new SqliteConnection(dbName);//creates connection to database using the location stored above
            connection.Open();//opens connection
            SqliteCommand Command = connection.CreateCommand();//allows commands to be created for the database
            sql = "SELECT question_status FROM Question_status WHERE question_id = " + data + " AND player_id = " + LoginController.playerId + "";//a query to find the status of the question based on its id, which is passed in as the variable data
            Command.CommandText = sql;//sets the sql variable to the command text
            SqliteDataReader reader = Command.ExecuteReader();//execute query above
            reader.Read();//reads data from the database
            int state = reader.GetInt32(0);//gets the question status from the database and stores it in state
            connection.Close();//closes the database connection
            return state;//returns the value of state (to the node in the linked list)
        }
        public int setlength()
        {
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT COUNT(question_id) FROM Question WHERE subject_id = " + Controller.subjectId + "";//counts the total number of records I have in my database from the question_id field
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int length = reader.GetInt32(0);
            connection.Close();
            return length;
        }

        public int[] setCounter()
        {
            int[] counter = new int[setlength()];
            int i = 0;
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT question_id FROM Question WHERE subject_id = "+Controller.subjectId+"";//counts the total number of records I have in my database from the question_id field
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                counter[i] = reader.GetInt32(0);//sets the counter variable to total number of records, which was read from the database
                i = i+1;
            }
           connection.Close();
           return counter;
        }

        public string getQuestion(int data)
        {
            questionId = data;
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT question FROM Question WHERE question_id = " + data + "";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            string question = reader.GetString(0);//stores the question as a string in the varible question
            connection.Close();
            return question;
        }

        public string getAnswer(int data)
        {
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT answer FROM Answer WHERE question_id = " + data + "";//uses the data passed in from the linked list class, that data is then used as the question id which will help to find the question
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            string answer = reader.GetString(0);//stores the question as a string in the varible question
            connection.Close();
            return answer;
        }

        public void generateQuestions()
        {
            int[] counter =setCounter();
            for (int i = 0; i < counter.Length; i++)//uses a for loop to repeat this section of code until all data has been added to the queue
            {
                list.addSort(counter[i]);//enqueues all the records ids, represented as the Int I, from the database and stores them in different nodes, sorted by question status(setData method)
            }
            
        }

        public string[] updateQuestion()
        {
            int previousId = questionId;
            currentQuestion=list.removeStart();
            if(currentQuestion == null){
                generateQuestions();
            }
            if(previousId!=0){
                if(setData(previousId) == 3){
                list.addSort(previousId);
                }
            }
            return currentQuestion;
        }
        public string[] updateAnswers()
        {
            string[] answers = new string[4];
            int[] answerId = new int[3];
            int[] counter = setCounter();
            int length = counter.Length;
            answers[Random.Range(0, 3)] = currentQuestion[1];
            for (int i = 0; i < 3; i++)
            {
                answerId[i] = counter[Random.Range(1, length-1)];
            }
            for (int i= 0; i < answers.Length; i++)
            {
                int j = i;
                if(j > 2){
                    j= 2;
                }
                if(answers[i] != currentQuestion[1]){
                    answers[i] = getAnswer(answerId[j]);
                }
            }
            return answers;
        }

        public int totalQuestions(){
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "SELECT COUNT(question_id) FROM Question";//counts the total number of records I have in my database from the question_id field
            Command.CommandText = sql;
            SqliteDataReader reader = Command.ExecuteReader();
            reader.Read();
            int total = reader.GetInt32(0);
            connection.Close();
            return total;
        }

        public void setNewStatus(int playerID)
        {
            int j=0;
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();
            for(int i=0; i < totalQuestions(); i++){
                j= i+1;

                sql = "INSERT INTO Question_status(question_status, question_id, player_id) VALUES("+0+", "+j+", "+playerID+")";

                Command.CommandText = sql;
                Command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public bool checkAnswer(string answer){
            if (answer == currentQuestion[1])
            {
                updateStatus(true);
                return true;
            }
            updateStatus(false);
            return false;
        }

        public void updateStatus(bool correct)
        {
            int status = setData(questionId);
            if (correct==true && status !=0){
                status = status-1;
            }
            else if (status != 3)
            {
                status = status+1;
            }
            SqliteConnection connection = new SqliteConnection(dbName);
            connection.Open();
            SqliteCommand Command = connection.CreateCommand();

            sql = "UPDATE Question_status SET question_status = "+status+" WHERE question_id = "+questionId+" AND player_id = '"+LoginController.playerId+"'";

            Command.CommandText = sql;
            Command.ExecuteNonQuery();
            connection.Close();
        }
    }
}