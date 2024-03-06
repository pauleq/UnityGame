// Singleton

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _uiText;

    void Start()
    {
        updateHealth();
    }

    public void updateHealth()
    {
        _healthText.text = "health: " + GameManager.gameManager._playerHealth.Health;
    }

    public void updateExpPoints()
    {
        _uiText.text = "exp: " + GameManager.gameManager._playerExpPoints.ExpPoints;
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
