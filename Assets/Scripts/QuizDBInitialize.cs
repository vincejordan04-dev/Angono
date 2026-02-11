using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using TMPro;
using UnityEditor;

public class QuizDBInitialize : MonoBehaviour
{

    private string dbPath;
    private void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/quiz.db";
        Debug.Log(dbPath);
        if (!File.Exists(Application.persistentDataPath + "/quiz.db"))
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
                // 1. Create the Table
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Quiz (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                description TEXT NOT NULL,
                correct TEXT NOT NULL,
                wrong1 TEXT NOT NULL,
                wrong2 TEXT NOT NULL,
                wrong3 TEXT NOT NULL,
                imageName TEXT
            );";
                command.ExecuteNonQuery();

                // 2. Insert rows individually to avoid multi-row syntax errors
                string[] queries = {
                "INSERT INTO Quiz (description, correct, wrong1, wrong2, wrong3, imageName) VALUES ('Question 1', 'Right', 'W1', 'W2', 'W3', 'pig');",
                "INSERT INTO Quiz (description, correct, wrong1, wrong2, wrong3) VALUES ('Question 2', 'Right', 'W1', 'W2', 'W3');",
                "INSERT INTO Quiz (description, correct, wrong1, wrong2, wrong3) VALUES ('Question 3', 'Right', 'W1', 'W2', 'W3');"
            };

                foreach (string query in queries)
                {
                    try
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("Row Insert Failed: " + e.Message);
                    }
                }

                Debug.Log("Database seeding attempt finished.");
            }
        }
    }
}