using UnityEngine;
using System.Collections;

public class AnimatedEffect : MonoBehaviour
{
	void OnDestroy()
	{
		Destroy(gameObject);
	}
}