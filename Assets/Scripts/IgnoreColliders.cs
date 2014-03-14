using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreColliders : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		UnityEngine.Object[] characterControllers = Object.FindObjectsOfType (typeof(CharacterController));

		foreach(CharacterController characterControllerA in characterControllers)
		{
			foreach(CharacterController characterControllerB in characterControllers) 
			{
				if(characterControllerA != characterControllerB) {
					characterControllerA.gameObject.SetActive(true);
					characterControllerB.gameObject.SetActive(true);
					Physics.IgnoreCollision(characterControllerA.collider, characterControllerB.collider);
				}

			}

		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
