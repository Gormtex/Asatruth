using UnityEngine;
using System.Collections;

public class RadHoboAI : MonoBehaviour {

    public Vector2 pushBack = new Vector2(300, 350);
    protected Rigidbody2D body2d;

    public float maxHealth = 100f;
    public float curHealth = 1f;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
	}

	void Update () {
	    if(curHealth <= 0) {
            Destroy(gameObject);
        }
	}

    public void Damage(int damage){
        curHealth -= damage;

    }

}
