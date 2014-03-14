using UnityEngine;
using System.Collections;

public class DeadPush : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(new Vector3(50, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
