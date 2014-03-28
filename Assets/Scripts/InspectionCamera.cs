using UnityEngine;
using System.Collections;

public class InspectionCamera : MonoBehaviour {
	public float moveSpeed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		}
	}
}
