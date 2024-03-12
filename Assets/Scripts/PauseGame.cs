using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    public PauseMenu pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Pause(); // pause game (for testing)
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Continue(); // continue game (for testing)
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.TurnPanelOn(); // turn pause GUI on
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        // Disable all the scripts while game is paused,
        // so no attributes are being changed
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }

        Debug.Log("Game paused.");
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        // Re-enable all scripts
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = true;
            }
        }

        pauseMenu.TurnPanelOff();
        Debug.Log("Game continuing.");
    }
}
