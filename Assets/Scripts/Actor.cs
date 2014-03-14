using UnityEngine;
using System.Collections;

public class Actor : uLink.MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	void Update() {


	}

	[RPC]
	public void SetActive(bool active) {

		gameObject.SetActive(active);
	}

	/*public void Shove() {
		Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, shoveRange);
		foreach(Collider collider in nearbyColliders) {
			collider.gameObject.GetComponent<Actor>().AddShoveForce();
		}
		
	}
	
	public void AddShoveForce() {
		//networkView.RPC("NetworkShoveForce",uLink.RPCMode.All);

	}

	/*[RPC]
	public void NetworkShoveForce() {

		ShoveForce shoveForce = gameObject.AddComponent<ShoveForce>() as ShoveForce;
		Vector3 normal = Vector3.Normalize(collider.transform.position - transform.position);
		shoveForce.normal = normal;

	}*/



}
