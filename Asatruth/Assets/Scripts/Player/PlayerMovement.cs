using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;

	private Walking walking;
	private Jumping jumping;
	private Crouching crouching;

	// Number of jumps at one time
	public int maxJumps = 2;
	// Current number of jumps
	private int jumpsUsed = 0;

	/*** Climb settings ***/
	// Are we climbing?
	protected bool bClimbing;
	// Can we climb?
	protected bool bCanClimb = false;
	protected GameObject toClimb;

	// Epsilon value for deciding if we are at the top of a jump
	private float topJumpEpsilon = 0.1f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		walking = GetComponent<Walking>();
		jumping = GetComponent<Jumping>();
		crouching = GetComponent<Crouching>();
	}

	void FixedUpdate()
	{
		HandleWalking();

		HandleJumping();
		HandleJumpingAnimations();

		HandleCrouching();

		//HandleClimbing();

		//HandleFalling();


		// Reset jump count
		if (jumping.IsGrounded() || bClimbing)
			jumpsUsed = 0;
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Climbable")
		{
			bCanClimb = true;
			toClimb = collider.gameObject;
		}
	}

	void OnCollisionExit2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Climbable")
			bCanClimb = false;
	}

	void HandleWalking()
	{
		float h = InputManager.GetAxis("Horizontal");
		walking.Walk(h);
		
		anim.SetBool("bWalking", (Mathf.Abs(rb.velocity.x) > 0.0f));
	}

	void HandleJumping()
	{
		bool bJump = InputManager.GetButtonDown("Jump");
		if (!bJump)
			return;

		// Can we jump?
		bool bCanJump = jumping.IsGrounded() || (jumpsUsed < (maxJumps - 1));
		if (bCanJump)
		{
			jumping.Jump();
			jumpsUsed++;
			
			anim.SetTrigger("tJumpUp");
			anim.SetBool("bJumping", true);
		}
	}

	void HandleJumpingAnimations()
	{
		if (jumping.IsJumping() && Mathf.Abs(rb.velocity.y) < topJumpEpsilon)
		{
			anim.SetTrigger("tJump");
		}
		
		if (rb.velocity.y < 0.0f)
		{
			anim.SetTrigger("tFalling");
		}

		if (jumping.JustLanded())
		{
			anim.SetBool("bJumping", false);
			anim.SetTrigger("tIdle");
		}
	}

	void HandleCrouching()
	{
		if (InputManager.GetButton("Crouch"))
		{
			crouching.Crouch();

			anim.SetBool("bCrouching", true);
		}
		else
		{
			crouching.Stand();

			anim.SetBool("bCrouching", false);
		}
	}

	void HandleFalling()
	{
		bool bFalling = !jumping.IsJumping() && (rb.velocity.y < 0.0f);

		if (bFalling)
			anim.SetTrigger("tFalling");
	}

	/*void HandleClimbing()
	{
		// Should we be climbing?
		if (bCanClimb)
		{
			float ySpeed = Mathf.Abs(rb.velocity.y);

			// If we are not climbing yet, we are not grounded (we are jumping), and are at the top of the jump,
			//		- then start climbing
			if (!bClimbing && !bGrounded && ySpeed < topJumpEpsilon)
			{
				bClimbing = true;
			}
		}

		if (bGrounded)
		{
			bClimbing = false;
		}
	}*/
}
