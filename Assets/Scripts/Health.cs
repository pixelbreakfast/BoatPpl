using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
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
					Die ();
				}
			}
		}
	}


	IEnumerator resetCanLoseHealth() {
		yield return new WaitForSeconds(0.1f);
		canLoseHealth = true;
	}

	void Die() {
		dead = true;

		GameObject ragdoll = GameObject.Instantiate(Resources.Load ("Prefabs/Dead Boat Person"), transform.position, transform.rotation) as GameObject;
		
		ragdoll.GetComponentInChildren<Renderer>().material.color = gameObject.GetComponentInChildren<Renderer>().material.color;
		gameObject.SetActive(false);
	}


}
