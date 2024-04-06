// Counts exp points for player and player alone (it's intended to, at least)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointCounter
{
    // Fields
    int _currentExpPoints;

	public Dictionary<string, int> totalStars = new Dictionary<string, int>(); // Track total stars per level

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


	public int CalculateStars(string levelName)
	{
		// Define thresholds for stars
		int[] starThresholds = { 1000, 5000, 10000 }; 
        int expCollected = _currentExpPoints;
		// Determine the number of stars based on exp collected
		int stars = 0;
		for (int i = 0; i < starThresholds.Length; i++)
		{
			if (expCollected >= starThresholds[i])
			{
				stars = i + 1;
			}
			else
			{
				break;
			}
		}

        totalStars.Add(levelName, stars);

		return stars;
	}
}
