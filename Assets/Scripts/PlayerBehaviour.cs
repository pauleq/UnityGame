using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        { 
            PlayerTakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHeal();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerAddExp(1000);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerRemoveExp(700);
        }
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Some debug level changing for easier testing for later levels

        //Move up a Level
		if (Input.GetKeyDown(KeyCode.P))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
        //Move down a level
		if (Input.GetKeyDown(KeyCode.O))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}

    private void PlayerTakeDamage(int damage = 1)
    {
        GameManager.gameManager.DamagePlayer(damage);
    }
    private void PlayerHeal(int healing = 1)
    {
        GameManager.gameManager.HealPlayer(healing);
    }
    private void PlayerRemoveExp(int exp)
    {
        GameManager.gameManager.RemoveExpPlayer(exp);
    }
    private void PlayerAddExp(int exp)
    {
        GameManager.gameManager.GiveExpPlayer(exp);
    }
}
