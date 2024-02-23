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
            //Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHeal();
            //Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerTakeDamage(int damage = 1)
    {
        GameManager.gameManager._playerHealth.DamageUnit(damage);
    }
    private void PlayerHeal(int healing = 1)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }
}
