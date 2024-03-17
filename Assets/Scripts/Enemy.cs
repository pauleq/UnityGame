using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] private GameObject leftBoundary;
	[SerializeField] private GameObject rightBoundary;
	[SerializeField] private float moveSpeed = 2f;

	private Rigidbody2D rb;
	private bool movingRight = true;
	private float leftCap;
	private float rightCap;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// Get left and right caps from GameObjects
		if (leftBoundary != null)
			leftCap = leftBoundary.transform.position.x;
		else
			Debug.LogWarning("Left boundary not assigned to Enemy script.");

		if (rightBoundary != null)
			rightCap = rightBoundary.transform.position.x;
		else
			Debug.LogWarning("Right boundary not assigned to Enemy script.");
	}

	void Update()
	{
		// Move the enemy
		if (movingRight)
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			if (transform.position.x >= rightCap)
			{
				Flip();
			}
		}
		else
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			if (transform.position.x <= leftCap)
			{
				Flip();
			}
		}
	}

	void Flip()
	{
		movingRight = !movingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
