using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    // Unity characteristics
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Enemy enemy;

    [SerializeField] private LayerMask jumpableGround;

    // Parameters
    [Header("Movement")]
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    private float moveMod = 0f;
    private float jumpMod = 0f;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public bool lockMovement = false;

    private enum MovementState { idle, running, jumping, falling }
    private MovementState state;

    [Header("Knockback")]
    public bool isKnockedBack = false;
    [SerializeField] private Transform _center;
    [SerializeField] private float knockbackForce = 15f;
    [SerializeField] private float knockbackDuration = 0.2f;

    [Header("Sounds")]
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource groundTouchSoundEffect;

    [Header("Misc")]
    [SerializeField] private float normalTreshold = 0.9f;

    public bool canEnd = false;



	// Start is called before the first frame update
	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

	}

    // Update is called once per frame
    private void Update()
    {

        if (!lockMovement && !isKnockedBack)
        {

            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * (moveSpeed + moveMod), rb.velocity.y);

            // Player jumps
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce + jumpMod);
            }

            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce + jumpMod);
                    jumpTimeCounter -= Time.deltaTime;
                } 
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            updateMovementState();
        }
        else
        {
            //Knockback velocity slowing down
            var lerpedXVelocity = Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3);
            rb.velocity = new Vector2(lerpedXVelocity, rb.velocity.y);
        }
    }

    // Player lands on ground
    void OnCollisionEnter2D(Collision2D collision)
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
                    groundTouchSoundEffect.Play();
                    break; // Exit loop after finding the first suitable contact point
                }
            }
        }

        if(collision.gameObject.tag == "Enemy")
        {
            if (state == MovementState.falling)
            {
                collision.gameObject.transform.Rotate(new Vector3(0, 0, 180));
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
                Destroy(collision.gameObject, 1f);
            }
            else
            {
                GameManager.gameManager.DamagePlayer(1);
                Knockback(collision.transform);

				/*Vector2 knockbackDirection = transform.position - collision.transform.position;
				knockbackDirection.Normalize();
				rb.velocity = new Vector2(knockbackDirection.x * knockbackForce, rb.velocity.y);

				lockMovement = true;
				Invoke(nameof(ResetKnockback), knockbackDuration);*/
			}
			
		}
    }

    // Updates player's movement state
    private void updateMovementState()
    {
        // Does the player run?
        if (dirX > 0f) // running to the right (->)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // running to the left (<-)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else // standing still
            state = MovementState.idle;

        if (rb.velocity.y > .1f)
            state = MovementState.jumping; // moving up with speed > .1f
        else if (rb.velocity.y < -.1f)
            state = MovementState.falling; // moving down with speed < -.1f

        anim.SetInteger("state", (int) state);
    }

    // Does player stand on the ground?
    private bool isGrounded() { return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Powerups")
        {
            Destroy(collision.gameObject);
            if (collision.gameObject.name == "JumpBoost")
            {
                sprite.color = new Color(0.5f, 0.5f, 1f, 1f);
                jumpMod = 5f;
            }
            else if (collision.gameObject.name == "SpeedBoost")
            {
                moveMod = 4f;
                sprite.color = new Color(1f, 0.92f, 0.016f, 1f);

			}
            else if (collision.gameObject.name == "ExtraHeart")
                GameManager.gameManager.HealPlayer(1);
		}
        if(collision.tag == "LevelEnd")
        {
			LevelChange levelChangeScript = FindObjectOfType<LevelChange>();
			levelChangeScript.LevelEndPickedUp();
			Destroy(collision.gameObject);
			
		}

	}

    public void DeathAnimation()
    {
        rb.velocity = new Vector2(0, jumpForce/2);
    }

	public void ResetDamaged()
    {
        jumpMod = 0f;
        moveMod = 0f;
	}

    public void Knockback(Transform t)
    {
        var dir = _center.position - t.position;
        isKnockedBack = true;
        rb.velocity = dir.normalized * knockbackForce;
        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}
