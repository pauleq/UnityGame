using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public int expRewardAmount = 100;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.gameManager.GiveExpPlayer(expRewardAmount);
            Destroy(gameObject);
        }
    }
}
