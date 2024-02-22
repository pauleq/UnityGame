using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject TempHealthCounter;
    
    void Start()
    {
        TempHealthCounter.GetComponent<TextMeshPro>().SetText("lol");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        { 
            PlayerTakeDamage();
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHeal();
            Debug.Log(GameManager.gameManager._playerHealth.Health);
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
