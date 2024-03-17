using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	//   //private GameObject left;
	////private GameObject right;

	//[SerializeField] private float leftCap;
	//[SerializeField] private float rightCap;

	//[SerializeField] private float jumpLength = 2;
	//[SerializeField] private float jumpHeight = 2;
	//public LayerMask Ground;

	//private Collider2D coll;
	//private Rigidbody2D rb;

	//private bool facingLeft = true;

	//// Start is called before the first frame update
	//void Start()
	//   {
	//	//float coordinates = leftCap.transform.position.x;
	//	coll = GetComponent<Collider2D>();
	//	rb = GetComponent<Rigidbody2D>();
	//	//leftCap = left.transform.position.x;
	//	//rightCap = right.transform.position.x;

	//}

	//// Update is called once per frame
	//void Update()
	//   {
	//	Debug.Log("Update method called.");

	//	if (facingLeft)
	//	{
	//		if (transform.position.x > leftCap)
	//		{
	//			if (transform.localScale.x != 1)
	//			{
	//				transform.localScale = new Vector3(1, 1, 1);
	//			}

	//			if (coll.IsTouchingLayers(Ground))
	//			{
	//				rb.velocity = new Vector2(-jumpLength, jumpHeight);
	//			}
	//		}
	//		else
	//		{
	//			facingLeft = false;
	//		}

	//	}
	//	else
	//	{
	//		if (transform.position.x < rightCap)
	//		{
	//			if (transform.localScale.x != -1)
	//			{
	//				transform.localScale = new Vector3(-1, 1, 1);
	//			}

	//			if (coll.IsTouchingLayers(Ground))
	//			{
	//				rb.velocity = new Vector2(jumpLength, jumpHeight);
	//			}
	//		}
	//		else
	//		{
	//			facingLeft = true;
	//		}
	//	}
	//   }

	//[SerializeField] private GameObject leftBoundary;
	//[SerializeField] private GameObject rightBoundary;
	//[SerializeField] private float moveSpeed = 2f;

	//private Rigidbody2D rb;
	//private bool movingRight = true;
	//private float leftCap;
	//private float rightCap;

	//void Start()
	//{
	//	rb = GetComponent<Rigidbody2D>();

	//}

	//void Update()
	//{
	//	// Move the enemy
	//	if (movingRight)
	//	{
	//		rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
	//		if (transform.position.x >= rightCap)
	//		{
	//			Flip();
	//		}
	//	}
	//	else
	//	{
	//		rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
	//		if (transform.position.x <= leftCap)
	//		{
	//			Flip();
	//		}
	//	}
	//}

	//void Flip()
	//{
	//	movingRight = !movingRight;
	//	Vector3 scale = transform.localScale;
	//	scale.x *= -1;
	//	transform.localScale = scale;
	//}

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
