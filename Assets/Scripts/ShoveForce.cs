using UnityEngine;
using System.Collections;

public class ShoveForce : MonoBehaviour {

	public Vector3 normal = new Vector3(0,0,0);
	float force = 6f;
	float falloff = 0.05f;
	float destroyThreshhold = 0.001f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController characterController = collider.gameObject.GetComponent<CharacterController>() as CharacterController;

		characterController.Move(normal * force * Time.deltaTime);
		force = force * (1 - falloff);

		if(force < destroyThreshhold) {
			Destroy (this);
		}



	}
}
