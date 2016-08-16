using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class PlayerMovement : CharacterMovement
{
	// Number of jumps at one time
	public int maxJumps = 2;
	// Current number of jumps
	private int jumpsUsed = 0;

	// Epsilon value for deciding if we are at the top of a jump
	private float topJumpEpsilon = 0.1f;

	void Update()
	{
		HandleMovement();
		HandleJumping();
		HandleClimbing();

		// Reset jump count
		if (bGrounded || bClimbing)
			jumpsUsed = 0;
	}

	void HandleMovement()
	{
		float h = InputManager.GetAxis("Horizontal");

		base.Move(h);
	}

	void HandleJumping()
	{
		bool bJump = InputManager.GetButtonDown("Jump");
		if (!bJump)
			return;

		// Can we jump?
		bool bCanJump = bGrounded || (jumpsUsed < (maxJumps - 1));
		if (bCanJump)
		{
			Jump();
			jumpsUsed++;
		}
	}

	void HandleClimbing()
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
	}
}
