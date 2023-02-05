using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static LevelManager LevelManager = new LevelManager();

    #region Awake - Singleton & Dont destroy on load
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);

        SceneManager.activeSceneChanged += OnSceneLoaded;
    }
    #endregion

    [Header("Level Management")]
    public Transform startPosition;
    [Header("Prefabs")]
    public GameObject player;

    public bool hasPlayer;

    public void OnEndPositionTriggered(int exitID)
    {
        ChangeScene("Level 2");
    }

    void OnLevelStart()
    {
        if (hasPlayer) { return; }
        startPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        if (startPosition != null)
        {
            Instantiate(player, startPosition);
        }
        else
        {
            Instantiate(player,Vector3.zero,Quaternion.identity);
        }

    }

    /// <summary>
    /// This is called when the scene is loaded
    /// </summary>
    public void OnSceneLoaded(Scene current, Scene next)
    {
        OnLevelStart();
    }

    /// <summary>
    /// This checks if next level can be loaded and calls LevelManager to load the next scene
    /// </summary>
    /// <param name="name"></param>
    void ChangeScene(string name)
    {
        //Check if objectives are filled if any
        
        LevelManager.LoadScene(name);
    }


}
