using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    private Transform _t;
    public float smoothing = 5f;
    public Vector3 offset;

    public bool follow;
    


    void Awake() {
        GetComponent<Camera>().orthographicSize = ((Screen.height / 2f) / 2f);
        GameObject.FindGameObjectsWithTag("Player");
    }
    // Use this for initialization
    void Start() {
        _t = target.transform;
        offset = new Vector3(0, 60, -10);

        //offset = transform.position - _t.position;
    }

    // Update is called once per frame
    void Update() {
        if (_t) {

            Vector3 targetCamPos = _t.position + offset;
            if (follow) {


                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            }
        }
    }
}
