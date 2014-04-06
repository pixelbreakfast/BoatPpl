using UnityEngine;
using System.Collections;

public class ScenicCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Messenger.AddListener("end_voyage", SetCameraPosition);

	}

	void SetCameraPosition() {

		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
		Camera.main.transform.parent = transform;
	}
}
