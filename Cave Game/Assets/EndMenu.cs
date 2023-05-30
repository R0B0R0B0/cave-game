using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndMenu : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button backButton = root.Q<Button>("backButton");

        backButton.clicked += () => Back();
    }

    void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
