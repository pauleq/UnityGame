// Singleton

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    [SerializeField] private TextMeshProUGUI _uiText;

    private int maxHP;
    [SerializeField] GameObject heartContainer;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject lostHeartPrefab;

    void Start()
    {
        maxHP = GameManager.gameManager._playerHealth.MaxHealth;
        updateHealth();
    }

    public void updateHealth()
    {
        DrawHeart(GameManager.gameManager._playerHealth.Health, maxHP);
    }

    public void updateExpPoints()
    {
        _uiText.text = "" + GameManager.gameManager._playerExpPoints.ExpPoints.ToString("000000");
    }

    public void DrawHeart (int hearts, int maxHearts)
    {
        Transform containerTrans = heartContainer.transform;
        foreach (Transform child in containerTrans) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHearts; i++)
        {

            if (i + 1 <= hearts)
            {
                GameObject heart = Instantiate(heartPrefab, containerTrans.position, Quaternion.identity);
                heart.transform.SetParent(containerTrans,false);
            } else {
                GameObject heart = Instantiate(lostHeartPrefab, containerTrans.position, Quaternion.identity);
                heart.transform.SetParent(containerTrans, false);
            }
        }
    }

    void Awake()
    {
        if (uiManager != null && uiManager != this)
        {
            Destroy(this);
        }
        else
        {
            uiManager = this;
        }
    }
}
