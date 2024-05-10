// Singleton

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    // Parameters
    [Header("Health")]
    public int startingHealth = 5;
    public int maxHealth = 5;
    private bool isdead = false;

    [Header("iFrames")]
    public bool isInvincible = false;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("GameObjects")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PCamera;

    [SerializeField] private AudioSource damagePlayerSoundEffect;
	[SerializeField] private AudioSource Backgroundmusic;
	[SerializeField] private AudioSource gameoverMusic;

	public GameObject GameoverUI;
    public UnitHealth _playerHealth = new UnitHealth(5, 5);
    public ExpPointCounter _playerExpPoints = new ExpPointCounter();
    public GameSaveData gameSaveData = new GameSaveData();


    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
            //DontDestroyOnLoad(gameManager);
        }

        spriteRend = Player.GetComponent<SpriteRenderer>();
        _playerHealth.Health = startingHealth;
        _playerHealth.MaxHealth = maxHealth;

        gameSaveData = new GameSaveData();
        if (SaveSystem.LoadData() != null)
            gameSaveData = SaveSystem.LoadData();
    }

    public void DamagePlayer(int damageAmount)
    {
        if (isInvincible) return;
        damagePlayerSoundEffect.Play();
        _playerHealth.DamageUnit(damageAmount);
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetDamaged();
        }
        if (_playerHealth.Health <= 0 && !isdead)
        {
            isdead = true;
            PCamera.GetComponent<CameraController>().followingPlayer = false;
            Player.GetComponent<PlayerMovement>().lockMovement = true;
            Player.GetComponent<SpriteRenderer>().sortingOrder = 11;
            Player.transform.Rotate(new Vector3(0, 0, 180));
            spriteRend.color = new Color(0.5f, 0.5f, 0.5f, 1);
            Player.GetComponent<GameRespawn>().enabled = false;
            Player.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(gameOver(playerMovement));

        }
        else StartCoroutine(Invulnerability());
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

    private IEnumerator Invulnerability()
    {
        isInvincible = true;
        spriteRend.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        isInvincible = false;
    }
    
    private IEnumerator gameOver(PlayerMovement pmove)
    {
        if(Backgroundmusic.isPlaying)
        {
            Backgroundmusic.Pause();
        }
        pmove.DeathAnimation();
        yield return new WaitForSeconds(1.5f);
        gameoverMusic.Play();
        GameoverUI.SetActive(true);
        Player.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
	}
}

