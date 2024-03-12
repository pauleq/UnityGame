using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void TurnPanelOn()
    {
        FindObjectOfType<PauseGame>().Pause();
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        FindObjectOfType<PauseGame>().Continue();
    }

    public void GoToMainMenu()
    {
        FindObjectOfType<PauseGame>().Continue(); // unfreeze scripts
        pausePanel.SetActive(false); // hide GUI window
        SceneManager.LoadScene(0); // Main menu scene
    }

    public void TurnPanelOff()
    {
        pausePanel.SetActive(false);
    }
}
