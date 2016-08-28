using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour
{
	// Particle effect to spawn when starting a jump
	public GameObject jumpParticleEffect;
	// The upward force for a jump
	public float jumpSpeed = 350.0f;

	// Handle to this game objects rigidbody
	protected Rigidbody2D rb;

	// Are we on the ground?
	protected bool bGrounded;
	// Are we mid jump?
	protected bool bJumping = false;
	// Did we just land?
	protected bool bJustLanded;
	// Are we falling?
	protected bool bFalling;

	// LayerMask describing what is ground
	public LayerMask groundMask;
	// Transform describing the bottom of the character (to check for ground collision)
	public Transform groundCheck;
	// The radius of circle used to check if the character is grounded
	private float groundRadius = 2.0f;

	// Epsilon value for deciding if we are at the top of a jump
	private float topJumpEpsilon = 0.1f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (bJustLanded)
			bJumping = false;
		bJustLanded = false;

		bGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

		if (bFalling && rb.velocity.y <= 0.0f)
		{
			// For one frame, bJumping and bJustLanded will both be true.
			// The frame after, both will be false.
			bJustLanded = true;
		}

		bFalling = (!bGrounded) && (rb.velocity.y < 0.0f);
	}

	// Are we on the ground?
	public bool IsGrounded()
	{
		return bGrounded;
	}

	// Are we jumping?
	public bool IsJumping()
	{
		return bJumping;
	}

	// Have we just landed on the ground?
	// Note: This function returns true if we have just landed from a jump OR a fall.
	public bool JustLanded()
	{
		return bJustLanded;
	}

	// Are we at the top of the jump?
	public bool AtTopOfJump()
	{
		return bJumping && (Mathf.Abs(rb.velocity.y) < topJumpEpsilon);
	}

	// Make this character jump.
	// @param angle - The angle for the jump. Defaults to PI/2 rads (up).
	public virtual void Jump(float angle = (Mathf.PI / 2.0f))
	{
		bJumping = true;

		//Vector2 force = new Vector2(Mathf.Cos(angle) * jumpForce, Mathf.Sin(angle) * jumpForce);
		//Debug.Log(force);
		//rb.AddForce(force);

		Vector2 velocity = new Vector2(Mathf.Cos(angle) * jumpSpeed, Mathf.Sin(angle) * jumpSpeed);
		rb.velocity += velocity;

		if (jumpParticleEffect != null)
		{
			// Spawn jumping particle effect
			Instantiate(jumpParticleEffect, transform.position, Quaternion.identity);
		}
	}
}
