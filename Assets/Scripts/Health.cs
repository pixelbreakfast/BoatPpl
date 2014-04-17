using UnityEngine;
using System.Collections;

public class Health : uLink.MonoBehaviour {
	public int health = 100;
	public bool invulnerable = true; //Flag that sets whether damage can be dealt at all
	bool canLoseHealth = true; // limits the frequency with which health is lost
	bool dead = false;
	
	// Update is called once per frame
	void Update() {

	}

	public void SetInvulnerability(bool invulnerable) {
		this.invulnerable = invulnerable;
	}

	public void SubtractHealth(int amount) {
		if(dead == false) {

				if(canLoseHealth && !invulnerable) {
					
					canLoseHealth = false;
					StartCoroutine("resetCanLoseHealth");

					health -= amount;
					if(health < 1) {
						
						dead = true;
						networkView.RPC("Die",uLink.RPCMode.All);
					}
				}
		}
	}



	IEnumerator resetCanLoseHealth() {
		yield return new WaitForSeconds(0.1f);
	
			canLoseHealth = true;

	}



}
