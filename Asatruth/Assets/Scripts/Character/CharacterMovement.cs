using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	protected Rigidbody2D rb;
	private SpriteRenderer sprite;

	// Move settings
	public float maxSpeed = 10.0f;

	// Grounded settings
	protected bool bGrounded;
	public Transform groundCheck;
	public LayerMask groundMask;
	private float groundRadius = 2.0f;

	// Jump settings
	private float jumpForce = 15000.0f;

	// Climb settings
	protected bool bClimbing;
	protected bool bCanClimb = false;
	protected GameObject toClimb;

	void Awake()
	{
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		bGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
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

	public virtual void Move(float h)
	{
		// Turn character if necessary
		if (Mathf.Abs(h) > Mathf.Epsilon)
			sprite.flipX = (h < 0.0f);

		rb.velocity = new Vector2(h * maxSpeed, rb.velocity.y);
	}

	public virtual void Jump()
	{
		rb.AddForce(new Vector2(0.0f, jumpForce));
	}
}
