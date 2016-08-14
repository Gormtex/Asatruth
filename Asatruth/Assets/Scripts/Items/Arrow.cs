using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Arrow : MonoBehaviour {

    public Vector2 initialVelocity = new Vector2(500, 0);
    public float timeElapsed;

    private PlayerSpecial rangePwr;
    private GameObject player;
    private Rigidbody2D body2d;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rangePwr = player.GetComponent<PlayerSpecial>();
    }
    // Use this for initialization
    void Start() {
        var startVelX = initialVelocity.x * transform.localScale.x;  //* PlayerSpecial.RangePwr;
        body2d.velocity = new Vector2(startVelX, initialVelocity.y);

    }

    void Update() {
        if (timeElapsed >= 5)
            Destroy(gameObject);
        timeElapsed += Time.deltaTime;
     }

    //void OnCollisionEnter2D(Collider2D col) {
    //    if (col.CompareTag("Enemy")) {
     //       Destroy(this.gameObject);
     //   }

    //}
}
