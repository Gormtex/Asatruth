using UnityEngine;
using System.Collections;

public class RadHoboDamage : MonoBehaviour {

    public float dmg = 20f;
    private PlayerDamaged player;
    private PlayerHealth playerH;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamaged>();
        playerH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.isTrigger != true && col.CompareTag("Player")) {
            col.SendMessageUpwards("Damage", dmg);
            // StartCoroutine(player.Knockback(0.02f, 350, player.transform.position));
        }

        if (col.CompareTag("Player")) {
            playerH.Damage(20);
            StartCoroutine(player.Knockback(0.02f, 350, player.transform.position));

        }

    }
}
