using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour
{
	protected Rigidbody2D rb;

	// Are we on the ground?
	protected bool bGrounded;
	// Are we mid jump?
	protected bool bJumping = false;
	// Did we just land?
	protected bool bJustLanded;
	// Are we falling?
	protected bool bFalling;

	public Transform groundCheck;
	public LayerMask groundMask;
	private float groundRadius = 2.0f;

	// Jump settings
	public GameObject jumpParticleEffect;
	public float jumpForce = 17500.0f;

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

		if (bFalling && bGrounded && rb.velocity.y <= 0.0f)
		{
			// For one frame, bJumping and bJustLanded will both be true.
			// The frame after, both will be false.
			bJustLanded = true;
		}

		bFalling = (!bGrounded) && (rb.velocity.y < 0.0f);
	}

	public bool IsGrounded()
	{
		return bGrounded;
	}

	public bool IsJumping()
	{
		return bJumping;
	}

	public bool JustLanded()
	{
		return bJustLanded;
	}

	public virtual void Jump()
	{
		bJumping = true;

		rb.AddForce(new Vector2(0.0f, jumpForce));

		if (jumpParticleEffect != null)
		{
			// Spawn jumping particle effect
			Instantiate(jumpParticleEffect, transform.position, Quaternion.identity);
		}
	}
}
