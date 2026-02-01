using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button myButton = root.Q<Button>("QBack");
        myButton.clicked += OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene("MainFunc");
    }
}

