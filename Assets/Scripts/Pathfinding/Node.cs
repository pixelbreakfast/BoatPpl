using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	public float occupancy = 1;
	public float proximityCutoff = 1f;
	public float weightFalloffAmount = 100;

	public static List<Node> GetNodesInRange(Vector3 position, float range) {

		List<Node> nodes = new List<Node>();
		Collider[] colliders = Physics.OverlapSphere(position,range) as Collider[];
		foreach(Collider collider in colliders) {
			if(collider.gameObject.tag == "Node") {
				nodes.Add (collider.gameObject.GetComponent<Node>()) ;

			}
		}

		return nodes;
	}

	public static List<Node> GetRandomNodesInRange(Vector3 position, float range) {
		
		List<Node> nodes = new List<Node>();
		Collider[] colliders = Physics.OverlapSphere(position,range) as Collider[];
		foreach(Collider collider in colliders) {
			if(collider.gameObject.tag == "Node") {
				nodes.Add (collider.gameObject.GetComponent<Node>()) ;
				
			}
		}
		nodes.Shuffle();
		return nodes;
	}

	void Start() {
		InvokeRepeating("UpdateVacancy", 0, 0.5f);
	

	}
	
	void UpdateVacancy() {
		if(GameManager.Instance == null) return;
		if (GameManager.Instance.actors == null) return;

		occupancy = 0;
		foreach(Actor actor in GameManager.Instance.actors) {
			if(actor != null) {

				float distance = Vector3.Distance(actor.transform.position, transform.position);
				if(distance < proximityCutoff) {
					occupancy += 1 - distance / proximityCutoff;
					
				}
			}


		}
		if(renderer != null) {
			transform.localScale = Vector3.one * occupancy;
		}
	}



}
