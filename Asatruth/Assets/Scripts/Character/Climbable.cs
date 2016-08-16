using UnityEngine;
using System.Collections;

public class Climbable : BasicMovement
{
	// Climb settings
	protected bool bCanClimb = false;
	protected GameObject toClimb;

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
}
