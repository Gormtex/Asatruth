using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {

    public float speed = 50f;
    public float runMultiplier = 2f;
    public bool running;
    //public PlayerAttack attacking;
    //public bool isAttacking;
    //public GameObject Player;

    //protected override void Awake() {

       // Player = GameObject.FindGameObjectWithTag("Player");
        //attacking = Player.GetComponent<PlayerAttack>();

    //}
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        running = false;

        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);
        //isAttacking = PlayerAttack.attacking;

        if (right || left) {
            
            var velX = speed * (float)inputState.direction;
            body2d.velocity = new Vector2(velX, body2d.velocity.y);

        }
        else if (collisionState.standing){
            body2d.velocity = new Vector2(0, body2d.velocity.y);
        }
        

    }
}
