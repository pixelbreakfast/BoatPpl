using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (Actor))]
public class PlayerController : ActorController {

	float moveSpeed = 2;
	public bool frozen = false;

	void Start () {


	}

	// Update is called once per frame
	void Update () {

		if(frozen) return;

		Vector3 move = Vector3.zero;

		if(Input.GetKey(KeyCode.W)) {
			move += transform.forward ;
		}
		
		if(Input.GetKey(KeyCode.D)) {
			move += transform.right ;
		}
		
		if(Input.GetKey(KeyCode.A)) {
			move += -transform.right;
		}
		
		if(Input.GetKey(KeyCode.S)) {
			move += -transform.forward;
		}

		
		if(Input.GetMouseButtonDown(0)) {
			gameObject.GetComponent<Actor>().Shove();
		}

		
		Move (move);

	}


	[RPC]
	public void moveCharacterController(Vector3 force) {
		Move (force);
	}
}
