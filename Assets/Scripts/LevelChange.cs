using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    public Image Image;
    public bool canEnd = false;
    public ExpPointCounter expCounter;

	public StarUI stars;
    [SerializeField] Animator transitionAnim;

    private void Start()
    {
        Image.enabled = false;
		expCounter = GameManager.gameManager._playerExpPoints;
	}

	public void LevelEndPickedUp()
	{
		canEnd = true;
        Image.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("EndOfLevel") && canEnd)
        {
			CompleteLevel();
			Debug.Log("Level Completed!");
		}

	}

	private void CompleteLevel()
	{

		int expCollected = 0;
		string levelName = SceneManager.GetActiveScene().name;
		switch (levelName)
		{
			case "Level 1":
				expCollected = 25000;
				break;
			case "Level 2":
				expCollected = 15150;
				break;
			case "Level 3":
				expCollected = 17050;
				break;
			case "Level 4":
				expCollected = 33500;
				break;
			case "Level 5":
				expCollected = 32200;
				break;
			default:
				Debug.LogError("Invalid level name: " + levelName);
				break;
		}


		if (SceneManager.GetActiveScene().name == "Level 5")
		{
			int starsObtained = expCounter.CalculateStars(SceneManager.GetActiveScene().name, expCollected);
			PlayerPrefs.SetInt("StarsObtained", starsObtained);
            GameManager.gameManager.gameSaveData.UpdateSave(SceneManager.GetActiveScene().buildIndex - 2, GameManager.gameManager._playerExpPoints.ExpPoints, expCounter.CalculateStars(SceneManager.GetActiveScene().name, expCollected, true));
            SaveSystem.SaveData(GameManager.gameManager.gameSaveData);
            SceneManager.LoadScene("EndStory");
			Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + PlayerPrefs.GetInt("StarsObtained", 0));
			//StartCoroutine(endOfLevel());
		}
		else 
		{
			int starsObtained = expCounter.CalculateStars(SceneManager.GetActiveScene().name, expCollected);
			PlayerPrefs.SetInt("StarsObtained", starsObtained);
			StartCoroutine(loadlevel());
			GameManager.gameManager.gameSaveData.UpdateSave(SceneManager.GetActiveScene().buildIndex - 2, GameManager.gameManager._playerExpPoints.ExpPoints, expCounter.CalculateStars(SceneManager.GetActiveScene().name, expCollected));
			SaveSystem.SaveData(GameManager.gameManager.gameSaveData);
		}
		Debug.Log(SceneManager.GetActiveScene().name);

	}

	IEnumerator loadlevel()
	{
		transitionAnim.SetTrigger("End");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		transitionAnim.SetTrigger("Start");
	}

	//IEnumerator endOfLevel()
	//{
	//	Debug.Log("End Of Level Coroutine Started");
	//	SceneManager.LoadScene("End");
	//	yield return new WaitForSeconds(1);

	//}
}
