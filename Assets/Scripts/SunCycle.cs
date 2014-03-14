using UnityEngine;
using System.Collections;

public class SunCycle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Vector3.zero);
		transform.RotateAround (Vector3.zero, Vector3.left, 1 * (Time.deltaTime/6));
	}
}
