using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Tilemap tilemap;

	private float tileSize;

	private void Start()
	{
		tileSize = tilemap.cellSize.y * tilemap.transform.localScale.y;
	}

	private void LateUpdate()
	{

		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

		float minY = tilemap.transform.position.y;
		float maxY = minY + tilemap.size.y * tileSize;

		Vector3 currentPosition = transform.position;
		currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
		transform.position = currentPosition;
	}
}