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
            PlayerTakeDamage();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
