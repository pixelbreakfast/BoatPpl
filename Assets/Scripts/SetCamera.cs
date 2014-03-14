using UnityEngine;
using System.Collections;

public class SetCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
		Camera.main.transform.parent = transform;
	}

}
