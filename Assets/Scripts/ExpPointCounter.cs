// Counts exp points for player and player alone (it's intended to, at least)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointCounter
{
    // Fields
    int _currentExpPoints;

    // Properties
    public int ExpPoints
    {
        get
        {
            return _currentExpPoints;
        }
        set
        {
            _currentExpPoints = value;
        }
    }

    // Constructor
    public ExpPointCounter()
    {
        _currentExpPoints = 0;
    }

    // Methods
    public void AddExpPoints(int expAmount)
    {
        _currentExpPoints += expAmount;
        if ( _currentExpPoints > 999999 ) 
        {
            _currentExpPoints = 999999; // Absolute limit that shouldn't even be possible to reach
        }

        UIManager.uiManager.updateExpPoints(); // Update exp points UI
    }
    public void RemoveExpPoints(int expAmount)
    {
        if (_currentExpPoints > 0)
        {
            _currentExpPoints -= expAmount;
        }
        if (_currentExpPoints < 0)
        {
            _currentExpPoints = 0;
        }

        UIManager.uiManager.updateExpPoints(); // Update exp points UI
    }
}
