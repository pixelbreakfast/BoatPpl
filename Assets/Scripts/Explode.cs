using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Transform[] allChildren = GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if(child.rigidbody != null) {
				child.rigidbody.AddExplosionForce(750,transform.position + new Vector3(0,-15,-5),100);
				child.rigidbody.AddRelativeTorque(new Vector3(Random.Range (25,75),0,0));
			}
		}
	}

}
