using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    public int GetTotalStars()
    {
        GameSaveData gameSaveData = new GameSaveData();

        if (SaveSystem.LoadData() != null)
            gameSaveData = SaveSystem.LoadData();

        int totalStars = 0;
        foreach (int stars in gameSaveData.stars)
        {
            totalStars += stars;
        }
        return totalStars;
    }

    public TextMeshProUGUI textMeshProUGUI;

	public TextMeshProUGUI starCountText;

	void Start()
	{
		int starsObtained = GetTotalStars();
		Debug.Log("Stars obtained: " + starsObtained);
		starCountText.text = string.Format("{0} / 15 stars obtained!", starsObtained);
	}
}
