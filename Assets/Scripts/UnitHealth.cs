// Can be used for many things, not just the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitHealth
{
    // Fields
    int _currentHealth;
    int _currentMaxHealth;

    // Properties
    public int Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }
    public int MaxHealth
    {
        get
        {
            return _currentMaxHealth;
        }
        set
        {
            _currentMaxHealth = value;
        }
    }

    // Constructor
    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth=health;
        _currentMaxHealth=maxHealth;
    }

    // Methods
    public void DamageUnit(int damageAmount = 1)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        // This is called even when the unit isn't the player. Shouldn't cause any bugs, since
        // it will just show the same health amount as already shown, but still kinda annoyingly
        // unoptimised
        UIManager.uiManager.updateHealth(); // Update UI
    }
    public void HealUnit(int healAmount = 1)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }

        // This is called even when the unit isn't the player. Shouldn't cause any bugs, since
        // it will just show the same health amount as already shown, but still kinda annoyingly
        // unoptimised
        UIManager.uiManager.updateHealth(); // Update UI
    }
}
