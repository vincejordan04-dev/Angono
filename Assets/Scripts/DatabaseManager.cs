using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    void Awake()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/admin.db";
        Debug.Log(Application.persistentDataPath);
        
    }

    public bool CheckAdminLogin(string username, string password)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT COUNT(*) FROM Admin WHERE username=@user AND password=@pass";

                command.Parameters.AddWithValue("@user", username);
                command.Parameters.AddWithValue("@pass", password);

                int count = System.Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
                
            }
            
        }
    
    }
    
}
