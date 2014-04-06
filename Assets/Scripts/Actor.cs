using UnityEngine;
using System.Collections;

public class Actor : uLink.MonoBehaviour {

	public bool playable = false;

	void Start() {
		Messenger.AddListener("start_voyage", StartVoyage);
		Messenger.AddListener("end_voyage", EndVoyage);
	}


	public void StartVoyage() {
		Health health = gameObject.GetComponent<Health>();
		
		if(health != null) {
			health.SetInvulnerability(false);
		} 

	}

	[RPC]
	public void Remove() {
		Destroy(gameObject);
	}

	public void EndVoyage() {
		if(playable) {
			GameObject winGUI = GameObject.Instantiate( Resources.Load ("GUI/WinGUI"), Camera.main.transform.position, Camera.main.transform.rotation ) as GameObject;
			winGUI.transform.parent = Camera.main.gameObject.transform;
		}

	}

	
	[RPC]
	public void SpawnBody () {
		
		GameObject ragdoll = GameObject.Instantiate(Resources.Load ("Dead Boat Person"), transform.position, transform.rotation) as GameObject;
		
		GameObject boatPersonHead = GetByTag("Head", gameObject);
		GameObject deadBoatPersonHead = GetByTag("Head", ragdoll);
		
		GameObject boatPersonBody = GetByTag("Body", gameObject);
		GameObject deadBoatPersonBody = GetByTag("Body", ragdoll);
		
		deadBoatPersonHead.renderer.material.mainTexture = boatPersonHead.renderer.material.mainTexture;
		deadBoatPersonBody.renderer.material.color = boatPersonBody.renderer.material.color;
	}
	
	GameObject GetByTag(string tagName, GameObject obj) {
		Transform[] children = obj.GetComponentsInChildren<Transform>();
		
		foreach(Transform child in children) {
			if(child.gameObject.tag == tagName) {
				return child.gameObject;
			}
		}
		return null;
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
