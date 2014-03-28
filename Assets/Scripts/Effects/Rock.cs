using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
	public float rotationAmount = 10;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		float currentSin = Mathf.Sin (Time.timeSinceLevelLoad) * rotationAmount;
		float previousSin = Mathf.Sin(Time.timeSinceLevelLoad - Time.deltaTime) * rotationAmount;
		float distance = currentSin - previousSin;
		Vector3 newRotation = new Vector3(0,0, distance);
		transform.Rotate(newRotation);



	}

}
