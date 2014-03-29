using UnityEngine;
using System.Collections;

public class Health : uLink.MonoBehaviour {
	public int health = 100;
	bool canLoseHealth = true;
	bool dead = false;

	
	// Update is called once per frame
	void Update () {
	
	}

	public void SubtractHealth(int amount) {
		if(dead == false) {

			if(canLoseHealth) {

				canLoseHealth = false;

				StartCoroutine("resetCanLoseHealth");
				health -= amount;

				if(health < 1) {
					dead = true;
					Die ();
				}
			}
		}
	}

	void Die() {

		SceneManager.Instance.actors.Remove(gameObject.GetComponent<Actor>());
		networkView.RPC ("SpawnBody",uLink.RPCMode.All);

		//ragdoll.GetComponentInChildren<Renderer>().material.color = gameObject.GetComponentInChildren<Renderer>().material.color;
		networkView.RPC ("SetActive",uLink.RPCMode.All,false);
		 
	}


	IEnumerator resetCanLoseHealth() {
		yield return new WaitForSeconds(0.1f);
		canLoseHealth = true;
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

}
