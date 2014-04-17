using UnityEngine;
using System.Collections;

public class SetCamera : MonoBehaviour {

	public GameObject head;

	// Use this for initialization
	void Start () {

		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
		Camera.main.transform.parent = transform;

		MouseLook mouseLook = Camera.main.gameObject.AddComponent<MouseLook>() as MouseLook;
		mouseLook.axes = MouseLook.RotationAxes.MouseY;
		mouseLook.sensitivityY = 10;
		mouseLook.minimumY = -60;
		mouseLook.maximumY = 60;

		InvokeRepeating("AdjustPosition", 0, 0.1f);
	}

	void AdjustPosition() {
		transform.position = head.transform.position;
	}

}
