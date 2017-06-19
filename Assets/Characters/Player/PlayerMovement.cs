using UnityEngine;

namespace RPG.Characters
{
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class PlayerMovement : MonoBehaviour
	{
		ThirdPersonCharacter character;
		Transform camTransform;
		bool usingGamepad = false;
		bool isJumping = false;
		bool isSprinting = false;

		void Start()
		{
			character = GetComponent<ThirdPersonCharacter>();
			camTransform = Camera.main.transform;
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.G))
			{
				usingGamepad = !usingGamepad;
			}

			isJumping = Input.GetButton("Jump");
		}

		// Fixed update is called in sync with physics
		void FixedUpdate()
		{
			ProcessMovement();
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
			character.Move(move);
		}
	}
}