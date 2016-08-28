using UnityEngine;
using System.Collections;

public class WallClimbing : MonoBehaviour
{
	// Are we climbing?
	protected bool bClimbing;
	// Can we climb?
	protected bool bCanClimb = false;

	// A handle to this character jumping action
	protected Jumping jumping;
	// A handle to this game object's rigidbody
	protected Rigidbody2D rb;
	// A handle to this game object's animator
	protected Animator anim;

	// Original gravity scale
	private float defaultGravityScale;

	// The offset of the character's hands to it's origin position (for when hanging off of a ledge)
	public Vector3 ledgeOffset;

	void Start()
	{
		jumping = GetComponent<Jumping>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		defaultGravityScale = rb.gravityScale;
	}

	void OnCollisionStay2D(Collision2D c)
	{
		if (c.gameObject.tag == "Climbable")
		{
			bCanClimb = true;
		}
	}

	void OnCollisionExit2D(Collision2D c)
	{
		if (c.gameObject.tag == "Climbable")
		{
			bCanClimb = false;
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Ledge")
		{
			Vector3 newPosition = c.transform.position + ledgeOffset;
			rb.MovePosition(newPosition);
			StickToWall();
		}
	}

	public bool IsClimbing()
	{
		return bClimbing;
	}

	void FixedUpdate()
	{
		// Should we be climbing?
		if (bCanClimb)
		{
			// If we are not climbing yet, we are not grounded, and are not on the ascension of a jump,
			//		- then start climbing
			bool bNotAscending = (rb.velocity.y <= 0.0f) || jumping.AtTopOfJump();
			if (!bClimbing && !jumping.IsGrounded() && bNotAscending)
			{
				bClimbing = true;
			}
		}

		if (jumping.IsGrounded())
		{
			if (bClimbing)
				anim.SetTrigger("tIdle");

			bClimbing = false;
			rb.gravityScale = defaultGravityScale;
		}
	}

	public virtual void StickToWall()
	{
		rb.gravityScale = 0.0f;
		rb.velocity = Vector2.zero;
	}

	public virtual void Fall()
	{
		rb.gravityScale = defaultGravityScale / 2.0f;
	}

	public virtual void StopClimbing()
	{
		bClimbing = false;
		rb.gravityScale = defaultGravityScale;
	}
}
