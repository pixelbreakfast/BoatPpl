using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contact : uLink.MonoBehaviour {
	public bool canRepel = false;

	ActorController actorController;
	CapsuleCollider capsuleCollider;
	
	float repelThreshhold = 0.4f;
	float maxForce = 1;

	Health health;

	//List<Collider> ignore = new List<Collider>();

	// Use this for initialization
	void Start () {
		Messenger.AddListener("start_voyage", SetCanRepel);

		capsuleCollider = GetComponent<CapsuleCollider>() as CapsuleCollider;

		actorController = transform.gameObject.GetComponent<ActorController>();

		health = transform.GetComponent<Health>() as Health;


		//InvokeRepeating("CheckContact", 0, 0.05f);
	}
	
	void Update() {
		CheckContact();

	}

	void SetCanRepel() {
		canRepel = true;
	}

	void CheckContact() {
	
		Actor[] actors = GameObject.FindObjectsOfType<Actor>() as Actor[];
		for(int i = 0; i< actors.Length; i++) {

			if(actors[i].gameObject == gameObject) continue;

			float distance = Vector3.Distance(actors[i].transform.position, transform.position);

			if(distance < repelThreshhold) {

				if(health != null) {

					health.SubtractHealth(2);

				} 
				float force = (1 - distance/repelThreshhold) * maxForce;

				Vector3 vector3Force = Vector3.Normalize(actors[i].transform.position - transform.position) * force ;
			
				actorController.AddBufferedMove(-vector3Force);
			}

		}
	}



}
