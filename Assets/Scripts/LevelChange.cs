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
			Debug.Log("AAAAA");
		}

	}

	private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
