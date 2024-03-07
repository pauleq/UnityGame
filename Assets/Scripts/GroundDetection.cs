using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    // Variable to store the last contact position
    public Vector2 lastContactPosition { get; set; }

    [SerializeField] private float normalTreshold = 0.9f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateLastContactPosition(collision);
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        UpdateLastContactPosition(collision);
	}

    void UpdateLastContactPosition(Collision2D collision)
    {
        // Check if the object colliding with the player is ground
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Check if the collision contact normal is pointing upwards
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.normal.y > normalTreshold) // Adjust this threshold as needed
                {
                    // Save the last contact position
                    lastContactPosition = contactPoint.point;
                    break; // Exit loop after finding the first suitable contact point
                }
            }
        }
    }
}
