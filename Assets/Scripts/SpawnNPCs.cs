using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnNPCs : MonoBehaviour {
	public Transform spawnLocation;
	public Grid grid;
	public GameObject proxyPrefab;
	public GameObject creatorPrefab;
	public int numberOfNPCs = 20;
	public float spawnTime = 0.8f;
	int npcCount = 0;


	// Use this for initialization
	public void Start () {


		InvokeRepeating("SpawnNPC",spawnTime,spawnTime);
	}

	void SpawnNPC() {
		if(SceneManager.Instance.actors.Count < numberOfNPCs) {	

			GameObject newNPC = uLink.Network.Instantiate(proxyPrefab,creatorPrefab,spawnLocation.position,spawnLocation.rotation,0,"") as GameObject;
						
			SceneManager.Instance.actors.Add (newNPC.GetComponent<Actor>());
			newNPC.GetComponent<AIController>().currentGrid = grid;


		}
	}

}
