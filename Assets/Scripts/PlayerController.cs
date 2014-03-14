using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : uLink.MonoBehaviour {

	CharacterController controller;
	 float moveSpeed = 2;
	 float gravity = 5;

	void Start () {
		if(controller == null) {
			controller = gameObject.GetComponent<CharacterController>() as CharacterController;
		}
	}

	// Update is called once per frame
	void Update () {

		Vector3 move = Vector3.zero;
		move += new Vector3(0, -gravity  * Time.deltaTime, 0);

		if(Input.GetKey(KeyCode.W)) {
			move += transform.forward * moveSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.D)) {
			move += transform.right * moveSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.A)) {
			move += -transform.right * moveSpeed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.S)) {
			move += -transform.forward * moveSpeed * Time.deltaTime;
		}

		
		controller.Move (move);
	}

	[RPC]
	public void moveCharacterController(Vector3 force) {
		controller.Move (force);
	}
}
