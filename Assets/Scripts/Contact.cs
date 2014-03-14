using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contact : uLink.MonoBehaviour {
	public float repelThreshhold = 0.4f;
	public float maxForce = 0.01f;
	List<Collider> colliders = new List<Collider>();
	//List<Collider> ignore = new List<Collider>();

	// Use this for initialization
	void Start () {

		Physics.IgnoreCollision(transform.parent.collider, collider);
		//ignore.Add(transform.parent.GetComponent<CharacterController>().collider);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Collider collider in colliders) {

			float distance = Vector3.Distance(collider.transform.position, transform.position);

			if(distance < repelThreshhold) {
				Health health = transform.parent.GetComponent<Health>() as Health;
				if(health != null) health.SubtractHealth(2);

				float force = (1 - distance/repelThreshhold) * maxForce;
				Vector3 vector3Force = Vector3.Normalize(collider.transform.position - transform.position) * force;
				CharacterController characterController = collider.transform.gameObject.GetComponent<CharacterController>() as CharacterController;
					if(characterController != null && characterController.gameObject.activeInHierarchy) 
					{
						if(transform.parent.gameObject.GetComponent<uLinkNetworkView>().isMine) {
						characterController.Move(vector3Force);
						} else {
							Debug.Log ("moving networked actor");
						}
						
					}
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		colliders.Add (other);

	}

	void OnTriggerExit(Collider other) {

		for (int i = 0; i < colliders.Count; i++) // Loop through List with for
		{
			if(colliders[i] == other) {
				colliders.Remove (colliders[i]);
			}

		}

	}
}
