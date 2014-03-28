﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contact : uLink.MonoBehaviour {
	public float repelThreshhold = 0.4f;
	public float maxForce = 0.01f;
	List<Collider> colliders = new List<Collider>();
	CharacterController characterController;
	Health health;

	//List<Collider> ignore = new List<Collider>();

	// Use this for initialization
	void Start () {
		characterController = transform.parent.GetComponent<CharacterController>() as CharacterController;
		health = transform.parent.GetComponent<Health>() as Health;

		Physics.IgnoreCollision(transform.parent.collider, collider);
		//ignore.Add(transform.parent.GetComponent<CharacterController>().collider);
	
		//InvokeRepeating("CheckContact", 0, 0.05f);
	}
	
	void Update() {
		CheckContact();

	}

	void CheckContact() {
		
		foreach(Collider collider in colliders) {
			
			float distance = Vector3.Distance(collider.transform.position, transform.position);
			
			if(distance < repelThreshhold) {
				
				//health.SubtractHealth(2);
				
				float force = (1 - distance/repelThreshhold) * maxForce;
				Vector3 vector3Force = Vector3.Normalize(collider.transform.position - transform.position) * force;
				
				
				if(characterController != null && characterController.gameObject.activeInHierarchy) 
				{
					characterController.Move(-vector3Force);
					
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
