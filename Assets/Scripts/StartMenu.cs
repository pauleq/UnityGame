using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    int startLevel = 1;

    private void Start()
    {
        GameManager.gameManager.gameSaveData = SaveSystem.LoadData();
        Debug.Log(GameManager.gameManager.gameSaveData.ToString());

        for (int i = 0; GameManager.gameManager.gameSaveData.levelFinished[i] == true; i++, startLevel++) ;

        Debug.Log("Player can continue from level " + startLevel);

        GameObject buttonObject = GameObject.Find("Button");

        if (buttonObject != null)
        {
            Button button = buttonObject.GetComponent<Button>();

            if (button != null)
                button.GetComponentInChildren<Text>().text = "Continue from level " + startLevel.ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + startLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
