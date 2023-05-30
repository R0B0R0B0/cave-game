using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button playButton = root.Q<Button>("playButton");
        Button settingsButton = root.Q<Button>("settingsButton");
        Button quitButton = root.Q<Button>("quitButton");

        playButton.clicked += () => StartGame();
       // settingsButton.clicked += () => StartGame();
        quitButton.clicked += () => QuitGame();
    }

    void StartGame() 
    {
        Debug.Log("Loading Scene Level");
        SceneManager.LoadScene("DemoLevel");
    }

    void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
