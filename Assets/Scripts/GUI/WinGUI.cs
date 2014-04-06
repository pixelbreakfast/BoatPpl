using UnityEngine;
using System.Collections;

public class WinGUI : MonoBehaviour {

	float timer = 0;
	float moveSpeed = 2;

	// Use this for initialization
	void Start () {


	}

	void Update() {
		if(timer < 1) {
		
			transform.position += transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime;
		}
		timer += Time.deltaTime;
	}

}
