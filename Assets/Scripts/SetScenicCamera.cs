using UnityEngine;
using System.Collections;

public class SetScenicCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Messenger.AddListener("end_voyage", DelayedSetScenicCamera);
	}
	
	// Update is called once per frame
	void DelayedSetScenicCamera () {
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
		Camera.main.transform.parent = transform;

		Destroy (Camera.main.GetComponent<MouseLook>());

	}
}
