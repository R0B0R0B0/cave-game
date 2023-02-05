using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager
{

    /// <summary>
    /// This loads the wanted scene and will save in the future maybe...
    /// </summary>
    /// <param name="scene">Put the wanted scenes name here</param>
    public void LoadScene(string scene)
    {
        Debug.Log(scene);

        //save??

        SceneManager.LoadScene(scene);
    }
}
