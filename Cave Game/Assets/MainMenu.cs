using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }
}
