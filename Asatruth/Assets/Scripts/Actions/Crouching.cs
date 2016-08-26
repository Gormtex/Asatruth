using UnityEngine;
using System.Collections;

public class Crouching : MonoBehaviour
{
	public Collider2D[] normalColliders;
	public Collider2D[] duckingColliders;

	public virtual void Crouch()
	{
		foreach (Collider2D c in normalColliders)
			c.enabled = false;

		foreach (Collider2D c in duckingColliders)
			c.enabled = true;
	}

	public virtual void Stand()
	{
		foreach (Collider2D c in normalColliders)
			c.enabled = true;

		foreach (Collider2D c in duckingColliders)
			c.enabled = false;
	}
}
