using UnityEngine;
using System.Collections;

public class PlayerHealth : AbstractBehavior {

    private Animator anim;

    public float curHealth;
    public float maxHealth = 100;
    public bool dead = false;

    override protected void Awake() {
        anim = GetComponent<Animator>();

    }

    // Use this for initialization
    void Start () {
        curHealth = maxHealth;
	}

    public void Damage(int damage) {
        curHealth -= damage;

    }
    // Update is called once per frame
    void Update () {

	
        if (curHealth > maxHealth) {
            curHealth = maxHealth;
        }

        if (curHealth <= 0) {
            OnDeath();
        }
        else {
            anim.SetBool("Dead", false);
            dead = false;
            ToggleScripts(true);


        }
    }
    void OnDeath() {

        dead = true;
        anim.SetBool("Dead", true);
        anim.SetInteger("AnimState", 100);
        ToggleScripts(false);


    }
}
