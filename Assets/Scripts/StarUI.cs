using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

	public TextMeshProUGUI starCountText;

	void Start()
	{
		Debug.Log("Start method called in EndSceneScript");
		int starsObtained = PlayerPrefs.GetInt("StarsObtained", 0);
		Debug.Log("Stars obtained: " + starsObtained);
		starCountText.text = string.Format("{0} / 15 stars obtained!", starsObtained);
	}
}
