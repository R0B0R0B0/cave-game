using UnityEngine;

public class DemoSceneMusics : MonoBehaviour
{
    AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();
    }

    private void Update()
    {
        if (!AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
    }
}
