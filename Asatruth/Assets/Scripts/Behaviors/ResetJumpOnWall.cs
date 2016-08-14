using UnityEngine;
using System.Collections;

public class ResetJumpOnWall : AbstractBehavior {
    // not functioning
    public float jumpSpeed = 200f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;
    public GameObject dustEffectPrefab;
    public bool isJumping;

    protected float lastJumpTime = 0;
    public int jumpsRemaining = 0;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    protected virtual void Update() {
        var canJump = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);
        
        if (collisionState.standing || collisionState.onWall) {
            isJumping = false;
            if (canJump && holdTime < .1f || collisionState.onWall) {
                jumpsRemaining = jumpCount - 1;
            }
        }
        else {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {
                if (jumpsRemaining > 0) {
                    
                    jumpsRemaining--;
                    var clone = Instantiate(dustEffectPrefab);
                    clone.transform.position = transform.position;

                }
            }
        }

    }
}
