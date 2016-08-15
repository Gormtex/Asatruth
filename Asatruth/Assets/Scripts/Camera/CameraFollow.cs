using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	// The game object to follow
	public GameObject target;
	// The offset of the camera to the target
	public float offset = 50.0f;
	// The smoothing of the camera's follow
	public float smoothing = 5.0f;

	// Convenience reference to the target's transform
	private Transform targetTransform;
	// Offset as a Vector3, so we only calculate it once
	private Vector3 vOffset;

	void Awake()
	{
		vOffset = new Vector3(0.0f, 0.0f, offset);
	}

	void Start()
	{
		GetComponent<Camera>().orthographicSize = ((Screen.height / 2f) / 2f);

		targetTransform = target.transform;
	}

	void Update()
	{
		Vector3 targetPos = targetTransform.position + vOffset;
		transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
	}	
}
