using UnityEngine;
using System.Collections;

public class RadHoboDamageTrigger : MonoBehaviour {

    public float dmg = 20f;
    public PlayerDamaged player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamaged>();
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.isTrigger != true && col.CompareTag("Player")) {
            col.SendMessageUpwards("Damage", dmg);           
        }

        //if (col.CompareTag("Player")) {
         //   StartCoroutine(player.Knockback(0.2f, 500, player.transform.position));

        //}

    }
}
