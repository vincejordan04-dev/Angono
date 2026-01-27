using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AdminLogin : MonoBehaviour
{
    public TMP_InputField AdminName;
    public TMP_InputField Pass;
    public int adminSceneIndex;

    public DatabaseManager db;

    public void Login()
    {
        Debug.Log("ADMIN LOGIN BUTTON CLICKED");


        if (db == null)
        {
            Debug.LogError("DatabaseManager missing");
            return;
        }

        if (db.CheckAdminLogin(AdminName.text, Pass.text))
        {
            Debug.Log("Login success");
            Time.timeScale = 1f;
            SceneManager.LoadScene(adminSceneIndex);
        }
        else
        {
            Debug.Log("Invalid credentials");
        }
    }
}
