using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	GameObject target = null;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		if (!target)
		{
			Debug.Log("Failed to located player object. Follow script will not function.");
		}
	}

	// Happens after physics have been calculated
	void LateUpdate()
	{
		if (!target)
		{
			return;
		}

		transform.position = target.transform.position;
	}
}
