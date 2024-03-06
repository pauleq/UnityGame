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
    public ExpPointCounter _playerExpPoints = new ExpPointCounter();

    [SerializeField] private GameObject Player;


    void Start()
    {

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

        _playerHealth.Health = startingHealth;
        _playerHealth.MaxHealth = maxHealth;
    }

    public void DamagePlayer(int damageAmount)
    {
        _playerHealth.DamageUnit(damageAmount);
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetDamaged();
        }
    }
    public void HealPlayer(int healAmount)
    {
        _playerHealth.HealUnit(healAmount);
    }
    public void GiveExpPlayer(int expAmount)
    {
        _playerExpPoints.AddExpPoints(expAmount);
    }
    public void RemoveExpPlayer(int expAmount)
    {
        _playerExpPoints.RemoveExpPoints(expAmount);
    }
}
