using System;
using UnityEngine;
//using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	//[SerializeField] float acceptanceRadius = 0.5f;
	//[SerializeField] const int walkableLayer = 8, enemyLayer = 9;

	ThirdPersonCharacter character;   // A reference to the ThirdPersonCharacter on the object
	Transform camTransform;
	//AICharacterControl ai;
	//CameraRaycaster raycaster;
	//Vector3 currentClickTarget;
	bool usingGamepad = false;
	bool isJumping = false;
	bool isSprinting = false;
	//GameObject groundTarget = null;

	void Start()
	{
		//raycaster = Camera.main.GetComponent<CameraRaycaster>();
		character = GetComponent<ThirdPersonCharacter>();
		camTransform = Camera.main.transform;
		//ai = GetComponent<AICharacterControl>();
		//currentClickTarget = transform.position;

		// Create ground target GO to use for movement
		//groundTarget = new GameObject("Ground Target");
		//raycaster.notifyMouseClickObservers += OnMouseClick;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			usingGamepad = !usingGamepad; // Toggle gamepad
										  //currentClickTarget = transform.position;
		}

		isJumping = Input.GetButton("Jump");
	}

	// Fixed update is called in sync with physics
	void FixedUpdate()
	{
		ProcessMovement();

		//if (usingGamepad)
		//{
		//	ProcessGamepadMovement();
		//}
	}

	void ProcessMovement()
	{
		// read inputs
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// calculate camera relative direction to move:
		var camForward = Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1)).normalized;
		var move = v * camForward + h * camTransform.right;

		// pass all parameters to the character control script
		character.ApplySprintFactor(Input.GetAxis("Sprint"));
		character.Move(move, false, isJumping);
	}

	//void OnMouseClick(RaycastHit hit, int layerHit)
	//{
	//	switch (layerHit)
	//	{
	//		case walkableLayer:
	//			SetMoveTarget(UpdateGroundTarget(hit.point));
	//			break;
	//		case enemyLayer:
	//			SetMoveTarget(hit.transform);
	//			break;
	//		default:
	//			return;
	//	}
	//}

	//Transform UpdateGroundTarget(Vector3 location)
	//{
	//	groundTarget.transform.position = location;
	//	return groundTarget.transform;
	//}

	//void SetMoveTarget(Transform target)
	//{
	//	ai.SetTarget(target);
	//}

	//void ProcessGamepadMovement()
	//{
	//	// read inputs
	//	float h = Input.GetAxis("Horizontal");
	//	float v = Input.GetAxis("Vertical");

	//	// calculate move direction to pass to character
	//	// calculate camera relative direction to move:
	//	var camForward = Vector3.Scale(raycaster.transform.forward, new Vector3(1, 0, 1)).normalized;
	//	var move = v * camForward + h * raycaster.transform.right;

	//	// pass all parameters to the character control script
	//	character.Move(move, false, false);
	//}

	//void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.black;
	//	Gizmos.DrawLine(transform.position, currentClickTarget);
	//	Gizmos.color = Color.green;
	//	Gizmos.DrawWireSphere(currentClickTarget, acceptanceRadius);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawSphere(currentClickTarget, 0.1f);
	//}
}