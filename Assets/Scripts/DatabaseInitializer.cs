using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseInitializer : MonoBehaviour
{
    private string dbPath;

    void Awake()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/admin.db";

        if (!File.Exists(Application.persistentDataPath + "/admin.db"))
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
                @"CREATE TABLE IF NOT EXISTS Admin (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT NOT NULL,
                    password TEXT NOT NULL
                );";
                command.ExecuteNonQuery();

                command.CommandText =
                "INSERT INTO Admin (username, password) VALUES ('admin', '1234');";
                command.ExecuteNonQuery();
            }
        }

        Debug.Log("Admin database created");
    }
}
