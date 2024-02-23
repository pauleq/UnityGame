// Can be used for many things, not just the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_currentHealth > 0) 
        { 
            _currentHealth -= damageAmount;
        }
    }
    public void HealUnit(int healAmount = 1)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth -= _currentMaxHealth;
        }
    }
}
