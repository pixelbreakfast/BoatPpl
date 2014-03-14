using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public float shoveRange;
	public float shoveForce;

	// Use this for initialization
	void Start () {
	}

	void Update() {


	}

	public void Shove() {
		Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, shoveRange);
		foreach(Collider collider in nearbyColliders) {
			if(collider.gameObject.tag == "Actor") {
				if(collider.gameObject != gameObject) {
					ShoveForce shoveForce = collider.gameObject.AddComponent<ShoveForce>() as ShoveForce;
					Vector3 normal = Vector3.Normalize(collider.transform.position - transform.position);
					shoveForce.normal = normal;
				}
			}

		}

	}


}
