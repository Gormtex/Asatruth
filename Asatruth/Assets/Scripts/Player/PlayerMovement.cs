using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	private Walking walking;
	private Jumping jumping;
	private Crouching crouching;
	private WallClimbing wallClimbing;

	// Number of jumps at one time
	public int maxJumps = 2;
	// Current number of jumps
	private int jumpsUsed = 0;

	public Transform top;

	private bool bJumpInput;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();

		walking = GetComponent<Walking>();
		jumping = GetComponent<Jumping>();
		crouching = GetComponent<Crouching>();
		wallClimbing = GetComponent<WallClimbing>();
	}

	void Update()
	{
		if (!bJumpInput)
			bJumpInput = InputManager.GetButtonDown("Jump");
	}

	void FixedUpdate()
	{
		bool bClimbing = wallClimbing.IsClimbing();
		anim.SetBool("bClimbing", bClimbing);
		if (!bClimbing)
		{
			HandleWalking();
			HandleJumping();
			HandleJumpingAnimations();
			HandleCrouching();
		}
		else
		{
			HandleClimbing();
		}

		// Reset jump count
		if (jumping.IsGrounded() || wallClimbing.IsClimbing())
			jumpsUsed = 0;

		bJumpInput = false;
	}

	void HandleWalking()
	{
		float h = InputManager.GetAxis("Horizontal");
		if (crouching.IsCrouching)
			return;

		walking.Walk(h);
		
		anim.SetBool("bWalking", (Mathf.Abs(rb.velocity.x) > 0.0f));
	}

	void HandleJumping()
	{
		if (!bJumpInput)
			return;

		// Can we jump?
		// Are we grounded or (since we are now mid air) do we have enough jumps left?
		bool bCanJump = (jumping.IsGrounded() || (jumpsUsed < (maxJumps - 1)));
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
		if (jumping.AtTopOfJump())
		{
			anim.SetTrigger("tJump");
		}

		bool bFalling = (rb.velocity.y < 0.0f);
		anim.SetBool("bFalling", bFalling);

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

	void HandleClimbing()
	{
		if (bJumpInput)
		{
			HandleWallJump();
			return;
		}

		float h = InputManager.GetAxis("Horizontal");
		bool bStick = ((h < 0.0f) == sprite.flipX);
		bool bFall = InputManager.GetButton("Crouch");

		if (bFall)
		{
			wallClimbing.Fall();
		}
		else if (bStick)
		{
			wallClimbing.StickToWall();
		}
		else if ((h > 0.0f) == sprite.flipX)
		{
			// Jump off the wall
			wallClimbing.StopClimbing();
			jumping.Jump(0.0f);
		}
	}

	void HandleWallJump()
	{
		wallClimbing.StopClimbing();

		float angle = (sprite.flipX) ? 60.0f * Mathf.Deg2Rad : 120.0f * Mathf.Deg2Rad;
		jumping.Jump(angle);

		jumpsUsed++;

		anim.SetTrigger("tJumpUp");
		anim.SetBool("bJumping", true);
	}
}
