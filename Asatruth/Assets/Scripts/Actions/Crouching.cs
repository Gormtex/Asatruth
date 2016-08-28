using UnityEngine;
using System.Collections;

public class Crouching : MonoBehaviour
{
	// The colliders to use while the character is not crouched
	public Collider2D[] normalColliders;
	// The colliders to use while the character is crouched
	public Collider2D[] duckingColliders;

	private bool bCrouching = false;
	public bool IsCrouching
	{
		get { return bCrouching; }
	}

	// Make this character crouch.
	public virtual void Crouch()
	{
		bCrouching = true;

		foreach (Collider2D c in normalColliders)
			c.enabled = false;

		foreach (Collider2D c in duckingColliders)
			c.enabled = true;
	}

	// Make this character stand.
	public virtual void Stand()
	{
		bCrouching = false;

		foreach (Collider2D c in duckingColliders)
			c.enabled = false;

		foreach (Collider2D c in normalColliders)
			c.enabled = true;
	}
}
