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
		
		GameObject ragdoll = uLink.Network.Instantiate(Resources.Load ("Dead Boat Person"), transform.position, transform.rotation,0) as GameObject;
		
		//ragdoll.GetComponentInChildren<Renderer>().material.color = gameObject.GetComponentInChildren<Renderer>().material.color;
		networkView.RPC ("SetActive",uLink.RPCMode.All,false);
	}


	IEnumerator resetCanLoseHealth() {
		yield return new WaitForSeconds(0.1f);
		canLoseHealth = true;
	}


}
