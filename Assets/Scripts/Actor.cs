using UnityEngine;
using System.Collections;

public class Actor : uLink.MonoBehaviour {

	public bool playable = false;
	float shoveRange = 1f;

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
	public void Die() {
		foreach(Transform child in gameObject.GetComponentsInChildren<Transform>() ) {
			if(child.camera != null) {
				child.camera.transform.parent = null;
				Destroy (Camera.main.GetComponent<MouseLook>());

			}


		}

		SpawnBody();

		if(uLink.Network.isServer) {

			uLink.Network.Destroy(gameObject);
		}
		
	}


	public void EndVoyage() {
		if(playable) {
			GameObject winGUI = GameObject.Instantiate( Resources.Load ("GUI/WinGUI"), Camera.main.transform.position, Camera.main.transform.rotation ) as GameObject;
			winGUI.transform.parent = Camera.main.gameObject.transform;
		}

	}


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

	public void Shove() {
		Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, shoveRange, 1 << LayerMask.NameToLayer("Actor"));
		foreach(Collider collider in nearbyColliders) {
			Vector3 normal  =  Vector3.Normalize(collider.transform.position - transform.position);

				collider.GetComponent<uLinkNetworkView>().RPC("AddShoveForce", uLink.RPCMode.All, normal);

		}
		
	}

[RPC]
	public void AddShoveForce(Vector3 normal) {
		ShoveForce shoveForce = collider.gameObject.AddComponent<ShoveForce>();
		shoveForce.normal = normal;
	}


}
