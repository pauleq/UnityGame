// Add to objects to make them hurt the player when touched

// Set isTrigger to true for bullets or enemies or other things you don't want to be collide-able

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObstacle : MonoBehaviour
{
    public int damageAmount = 1; // Amount of damage touching the object will do
    public bool destroyOnHit = false; // Destroy object after hurting player once

    // If isTrigger is false
    void OnCollisionStay2D(Collision2D collision)
    {
        TouchConsequences(collision.collider);
    }

    // If isTrigger is true
    void OnTriggerStay2D(Collider2D collider)
    {
        TouchConsequences(collider);
    }
    
    // Decides what happens when object is touched
    void TouchConsequences(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !GameManager.gameManager.isInvincible)
        {
            GameManager.gameManager.DamagePlayer(damageAmount);

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }

		}
	}
}
