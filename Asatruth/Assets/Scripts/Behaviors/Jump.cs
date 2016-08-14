using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

    public float jumpSpeed = 200f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;
    public GameObject dustEffectPrefab;
    public bool isJumping;
    private Animator anim;
    public WallJump jumpingOffWall;

    public CircleCollider2D circleCollider;
    public PolygonCollider2D polygonCollider;


    protected float lastJumpTime = 0;
    public int jumpsRemaining = 0;


    protected override void Awake() {
        base.Awake();

        circleCollider = GetComponent<CircleCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
        jumpingOffWall = GetComponent<WallJump>();


    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        var canJump = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);
        if (collisionState.onWall)
            jumpsRemaining = jumpCount;

        //if (Walljump.jumpingOffWall == true) {
        //    jumpsRemaining = jumpCount;
        //}

        if (collisionState.standing || collisionState.onWall) {
            anim.SetBool("Standing", true);

            isJumping = false;
            if (canJump && holdTime < .1f ) { // || collisionState.onWall

                jumpsRemaining = jumpCount - 1;
                OnJump();
            }
        }
        else {
            anim.SetBool("Standing", false);
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {
                if (jumpsRemaining > 0) {
                    OnJump();
                    jumpsRemaining--;
                    var clone = Instantiate(dustEffectPrefab);
                    clone.transform.position = transform.position;

                }
            }
        }
        
	}

    protected virtual void OnJump() {
        
        isJumping = true;
        var vel = body2d.velocity;
        lastJumpTime = Time.time;
        body2d.velocity = new Vector2(vel.x, jumpSpeed);
    }

}
