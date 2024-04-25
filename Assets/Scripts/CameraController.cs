using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Tilemap tilemap;
    [SerializeField] private AudioSource outOfBondsEffect;

    private float tileSize;
	public bool followingPlayer = true;

	private void Start()
	{
		tileSize = tilemap.cellSize.y * tilemap.transform.localScale.y;
	}

	private void LateUpdate()
	{
		if (followingPlayer)
		{
			transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

			float minY = tilemap.transform.position.y;
			float maxY = minY + tilemap.size.y * tileSize;

			Vector3 currentPosition = transform.position;

			if ((currentPosition.y == Mathf.Clamp(currentPosition.y, minY, maxY)) && outOfBondsEffect.isPlaying)
			{
                outOfBondsEffect.Stop();
                Debug.Log("stopping");
            }
			else if ((currentPosition.y != Mathf.Clamp(currentPosition.y, (minY-3f), maxY)) && !outOfBondsEffect.isPlaying)
            {
                outOfBondsEffect.Play();
                Debug.Log("Playing");
            }

			currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
			transform.position = currentPosition;
		}
	}
}