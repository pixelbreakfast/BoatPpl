using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class ActorController : uLink.MonoBehaviour{

	public CharacterController characterController;

	Vector3 bufferedMoveAmount = Vector3.zero;
	
	float walkSpeed = 1;
	
	float gravity = 5;

	void Awake() {
		characterController = GetComponent<CharacterController>();
	}

	public void AddBufferedMove(Vector3 moveAmount) {
		bufferedMoveAmount += moveAmount;
	}

	public void Move(Vector3 normal) {
		characterController.Move(((normal * walkSpeed) + bufferedMoveAmount) * Time.deltaTime);
		
		characterController.Move (new Vector3(0, -gravity, 0));
		bufferedMoveAmount = Vector3.zero;
	}
}
