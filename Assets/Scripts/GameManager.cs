// Singleton

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    // Parameters
    public int startingHealth = 5;
    public int maxHealth = 5;

    public UnitHealth _playerHealth = new UnitHealth(5, 5);


    void Start()
    {
        _playerHealth.Health = startingHealth;
        _playerHealth.MaxHealth = maxHealth;
    }

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
