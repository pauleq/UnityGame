using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Continue();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }

        Debug.Log("Game paused.");
    }

    private void Continue()
    {
        Time.timeScale = 1f;

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        Debug.Log("Game continuing.");
    }
}
