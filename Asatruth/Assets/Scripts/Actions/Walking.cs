using UnityEngine;
using System.Collections;

// InputManager
using TeamUtility.IO;

public class Walking : MonoBehaviour
{
	protected Rigidbody2D rb;
	private SpriteRenderer sprite;

	// Move settings
	public float maxSpeed = 10.0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	public virtual void Walk(float h)
	{
		// Turn character if necessary
		if (Mathf.Abs(h) > Mathf.Epsilon)
			sprite.flipX = (h < 0.0f);

		rb.velocity = new Vector2(h * maxSpeed, rb.velocity.y);
	}
}
