using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public GroundDetection groundDetection;

    [SerializeField]
    private float threshold;

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial position of the player
        groundDetection.lastContactPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 lastContactPosition = groundDetection.lastContactPosition;

        float distance = Vector2.Distance(currentPosition, lastContactPosition);

        Debug.Log("Distance from last contact position: " + distance);

        if (distance >= threshold)
        {
			// Calling damage function to damage the player when he falls
			// out of the world bounds 
			GameManager.gameManager.DamagePlayer(1);

			// Respawn the player at the initial position
			Vector2 respawnPosition = groundDetection.lastContactPosition;
            respawnPosition.y = groundDetection.lastContactPosition.y + 1;
            transform.position = respawnPosition;
            Debug.Log("Player teleported to: " + groundDetection.lastContactPosition);
        }
    }
}