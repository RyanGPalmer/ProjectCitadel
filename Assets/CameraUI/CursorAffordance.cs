using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
	[SerializeField] Texture2D cursorWalk, cursorAttack, cursorUnknown;
	[SerializeField] const int walkableLayer = 8, enemyLayer = 9;
	CameraRaycaster raycaster = null;

	// Use this for initialization
	void Start()
	{
		raycaster = GetComponent<CameraRaycaster>();
		if (!raycaster)
		{
			Debug.Log("Camera Raycaster not found by cursor affordance component. Cursor will not function.");
		}

		raycaster.notifyLayerChangeObservers += OnLayerChange; // Add delegate to raycaster's observer set
	}

	// Update is called once per frame
	void OnLayerChange(int newLayer)
	{
		switch (newLayer)
		{
			case walkableLayer:
				Cursor.SetCursor(cursorWalk, Vector2.zero, CursorMode.Auto);
				break;
			case enemyLayer:
				Cursor.SetCursor(cursorAttack, Vector2.zero, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(cursorUnknown, Vector2.zero, CursorMode.Auto);
				return;
		}
	}
}
