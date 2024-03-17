using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Unity characteristics
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // Parameters
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    private enum MovementState { idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource groundTouchSoundEffect;

    [SerializeField] private float normalTreshold = 0.9f;

    public bool canEnd = false;

	private MovementState state;

	private bool isKnockedBack = false;

	[SerializeField] private float knockbackForce = 15f; 
	[SerializeField] private float knockbackDuration = 0.2f;


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

        if (!isKnockedBack)
        {

            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            // Player jumps
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }


            updateMovementState();
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
                Destroy(collision.gameObject);
            }
            else
            {
                GameManager.gameManager.DamagePlayer(1);

				Vector2 knockbackDirection = transform.position - collision.transform.position;
				knockbackDirection.Normalize();
				rb.velocity = new Vector2(knockbackDirection.x * knockbackForce, rb.velocity.y);

				isKnockedBack = true;
				Invoke(nameof(ResetKnockback), knockbackDuration);
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
                jumpForce = 15f;
			else if (collision.gameObject.name == "SpeedBoost")
				moveSpeed = 11f;
            else if (collision.gameObject.name == "ExtraHeart")
                GameManager.gameManager.HealPlayer(1);
		}
        if(collision.tag == "LevelEnd")
        {
			LevelChange levelChangeScript = FindObjectOfType<LevelChange>();
			Destroy(collision.gameObject);
			levelChangeScript.LevelEndPickedUp();
		}

	}

	public void ResetDamaged()
    {
        jumpForce = 10f;
        moveSpeed = 7f;
	}

	private void ResetKnockback()
	{
		isKnockedBack = false;
	}
}
