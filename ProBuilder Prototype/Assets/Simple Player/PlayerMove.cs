using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float movementSpeed = 10f;

	private CharacterController charController;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
		float vertInput = Input.GetAxis("Vertical") * movementSpeed;

		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

		Vector3 moveDir = forwardMovement + rightMovement;
		moveDir += Physics.gravity;

		charController.Move(moveDir * Time.deltaTime);
	}

}