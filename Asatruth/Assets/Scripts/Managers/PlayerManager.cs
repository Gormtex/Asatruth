using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour{

    private InputState inputState;
    private Walk walkBehavior;
    private Animator animator;
    private CollisionState collisionState;
    private Duck duckBehavior;
    private Rigidbody2D body2d;
    private PlayerSpecial PS;
    public float jumpGap = 120f;
    
    
    void Awake() {
        inputState = GetComponent<InputState>();
        walkBehavior = GetComponent<Walk>();
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
        duckBehavior = GetComponent<Duck>();
        body2d = GetComponent<Rigidbody2D>();
        PS = GetComponent<PlayerSpecial>();


    }


    // Update is called once per frame
    void Update() {
        if (collisionState.standing) {
            ChangeAnimationState(0); //idle
        }

        if (inputState.absVelX > 0) {

            if (animator.GetBool("IsRunning") == false) {
                if (collisionState.onWall) {
                    ChangeAnimationState(15);
                }
                else {
                    ChangeAnimationState(11); 
                }
                animator.SetBool("IsRunning", true);
            }else           
                ChangeAnimationState(1); // run


        }
        else { 
            animator.SetBool("IsRunning", false);

        }

        if (!collisionState.standing && body2d.velocity.y >= -jumpGap && body2d.velocity.y <= jumpGap) {
            ChangeAnimationState(2); //mid
        }

        if (inputState.absVelY > 0 && body2d.velocity.y < -jumpGap) {
            ChangeAnimationState(8); //down
        }

        if (inputState.absVelY > 0 && body2d.velocity.y > jumpGap) {
            ChangeAnimationState(7); //up
        }

        animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;

        if (duckBehavior.ducking) {

            ChangeAnimationState(3); //duck
        }

        if (!collisionState.standing && collisionState.onWall) {
            ChangeAnimationState(4); //Onwall
            animator.SetBool("Attacking", false);

        }
//|| specialBehavior.healing || specialBehavior.punching || specialBehavior.magicing
        if (PS.shooting || PS.punching) {
            ChangeAnimationState(10); //special
        }
    }

    void ChangeAnimationState(int value) {
        animator.SetInteger("AnimState", value);
    }
}
