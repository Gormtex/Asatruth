using UnityEngine;
using System.Collections;

public class PlayerDamaged : MonoBehaviour {

    private Rigidbody2D rb2d;
    
	 void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir) {
        float timer = 0;
        while(knockDur > timer) {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y + knockbackPwr, transform.position.z));
        }
        yield return 0;
    }
}
