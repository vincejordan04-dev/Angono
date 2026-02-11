using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using TMPro;

public class QuizDBInitialize : MonoBehaviour
{
    private string dbPath;
    private void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/quiz.db";

        if (!File.Exists(dbPath))
        {
            CreateDatabase();
        }
    }

    void CreateDatabase()
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                @"CREATE TABLE IF NOT EXISTS Quiz (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    questionNumber TEXT NOT NULL,
                    description TEXT NOT NULL,
                    correct TEXT NOT NULL,
                    wrong1 TEXT NOT NULL,
                    wrong2 TEXT NOT NULL,
                    wrong3 TEXT NOT NULL
                );";
                command.ExecuteNonQuery();

                command.CommandText =
                @"INSERT INTO Quiz (questionNumber, description, correct, wrong1, wrong2, wrong3) VALUES
                    ('q1', 'desc', 'right' , 'wrong', 'wrong', 'wrong'), 
                    ('q1', 'desc', 'right' , 'wrong', 'wrong', 'wrong'),
                    ('q1', 'desc', 'right' , 'wrong', 'wrong', 'wrong')
                    ;";
                command.ExecuteNonQuery();
            }
        }

        Debug.Log("Quiz database created");
    }
}