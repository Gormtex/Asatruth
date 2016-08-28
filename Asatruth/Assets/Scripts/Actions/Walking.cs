using UnityEngine;
using System.Collections;

// InputManager
using TeamUtility.IO;

public class Walking : MonoBehaviour
{
	// Move settings
	public float maxSpeed = 10.0f;
	
	protected Rigidbody2D rb;
	private SpriteRenderer sprite;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	// Make this character walk.
	// @param h - A value in [-1, 1] deciding the walking speed, relative to maxSpeed (the sign will decide the direction).
	public virtual void Walk(float h)
	{
		// If h is~ 0, do nothing
		if (Mathf.Abs(h) < Mathf.Epsilon)
			return;

		// Turn character if necessary
		if (Mathf.Abs(h) > Mathf.Epsilon)
			sprite.flipX = (h < 0.0f);

		rb.velocity = new Vector2(h * maxSpeed, rb.velocity.y);
	}
}
