using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    int startLevel = 1;

    private void OnEnable()
    {
        GameSaveData gameSaveData = new GameSaveData();

        if (SaveSystem.LoadData() != null)
            gameSaveData = SaveSystem.LoadData();

        for (int i = 0; gameSaveData.levelFinished[i] == true; i++, startLevel++) ;

        GameObject buttonObject = GameObject.Find("Button");

        if (buttonObject != null && gameSaveData.levelFinished[0] != false)
        {
            Debug.Log("Player can continue from level " + startLevel);
            Button button = buttonObject.GetComponent<Button>();

            if (button != null)
                button.GetComponentInChildren<Text>().text = "Continue from level " + startLevel.ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StartStory");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
