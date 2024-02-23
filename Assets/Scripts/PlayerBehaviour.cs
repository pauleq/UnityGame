using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    void Start()
    {

    }

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
    }

    private void PlayerTakeDamage(int damage = 1)
    {
        GameManager.gameManager._playerHealth.DamageUnit(damage);
        UIManager.uiManager.updateHealth(); // Update UI
    }
    private void PlayerHeal(int healing = 1)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        UIManager.uiManager.updateHealth(); // Update UI
    }
}
