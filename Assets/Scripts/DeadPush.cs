using UnityEngine;
using System.Collections;

public class DeadPush : MonoBehaviour {

	// Use this for initialization
	void Start () {

		rigidbody.AddForce(new Vector3(Random.Range(-50f,50f), Random.Range(-50f,50f), Random.Range(-50f,50f)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
