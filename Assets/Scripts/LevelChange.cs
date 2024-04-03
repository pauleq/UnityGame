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

    [SerializeField] Animator transitionAnim;

    private void Start()
    {
        Image.enabled = false;
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
        StartCoroutine(loadlevel());
        GameManager.gameManager.gameSaveData.UpdateSave(SceneManager.GetActiveScene().buildIndex-1, GameManager.gameManager._playerExpPoints.ExpPoints);
        SaveSystem.SaveData(GameManager.gameManager.gameSaveData);

    }

    IEnumerator loadlevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }
}
