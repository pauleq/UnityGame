// Singleton

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    // Parameters
    [Header ("Health")]
    public int startingHealth = 5;
    public int maxHealth = 5;

    [Header("iFrames")]
    public bool isInvincible = false;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("GameObjects")]
    [SerializeField] private GameObject Player;

    public UnitHealth _playerHealth = new UnitHealth(5, 5);
    public ExpPointCounter _playerExpPoints = new ExpPointCounter();

    void Start()
    {

    }

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }

        spriteRend = Player.GetComponent<SpriteRenderer>();
        _playerHealth.Health = startingHealth;
        _playerHealth.MaxHealth = maxHealth;
    }

    public void DamagePlayer(int damageAmount)
    {
        if (isInvincible) return;
        _playerHealth.DamageUnit(damageAmount);
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetDamaged();
        }

        StartCoroutine(Invulnerability());
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
        for (int i=0; i<numberOfFlashes; i++) 
        {
            spriteRend.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        isInvincible = false;
    }
}
