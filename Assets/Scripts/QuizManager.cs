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
    public UnityEngine.UI.Image displayImage;
    private string dbPath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/quiz.db";
        DisplayQuiz();
    }

    public void DisplayQuiz()
    {
        // Clear text initially
        description.text = "Loading...";


        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Changed id = '0' to id = 1
                command.CommandText = "SELECT * FROM Quiz WHERE id = 1";

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Use 'if' since we only expect one row
                    {
                        description.text = reader["description"].ToString();
                        correct.text = reader["correct"].ToString();
                        wrong1.text = reader["wrong1"].ToString();
                        wrong2.text = reader["wrong2"].ToString();
                        wrong3.text = reader["wrong3"].ToString();
                        Debug.Log("Data loaded into UI successfully!");

                        string imgName = reader["imageName"].ToString();
                        // Unity looks inside any folder named "Resources" automatically
                        Sprite loadedSprite = Resources.Load<Sprite>("Images/" + imgName);

                        if (loadedSprite != null)
                        {
                            displayImage.sprite = loadedSprite;
                        }
                    }
                    else
                    {
                        description.text = "Error: Question not found.";
                        Debug.LogWarning("No row found with ID 1.");
                    }
                }
            }
        }
    }
}
    

