using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mono.Data.Sqlite;
using System.Xml.Schema;
using System.Data;
using System;
using Unity.Multiplayer.Center.Common;
public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI description;
    public TextMeshProUGUI correct;
    public TextMeshProUGUI wrong1;
    public TextMeshProUGUI wrong2;
    public TextMeshProUGUI wrong3;
    private string dbPath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/quiz.db";
        DisplayQuiz();
    }

    public void DisplayQuiz()
    {
        description.text = "";
        correct.text = "";
        wrong1.text = "";
        wrong2.text = "";
        wrong3.text = "";
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Quiz WHERE questionNumber = 'q1'";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        description.text = Convert.ToString(reader["description"]);
                        correct.text = Convert.ToString(reader["correct"]);
                        wrong1.text = Convert.ToString(reader["wrong1"]);
                        wrong2.text = Convert.ToString(reader["wrong2"]);
                        wrong3.text = Convert.ToString(reader["wrong3"]);
                    }
                    reader.Close();
                }
            }
        }
    }
}
