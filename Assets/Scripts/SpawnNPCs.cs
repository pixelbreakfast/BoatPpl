using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnNPCs : MonoBehaviour {
	public float spawnDistance;
	public Grid grid;
	public GameObject proxyPrefab;
	public GameObject creatorPrefab;

	public int width = 30;
	public int length = 30;
	float raycastRange = 100;
	float xOffset;
	float zOffset;

	// Use this for initialization
	public void Start () {
		
		xOffset = -width/2;
		zOffset = -length/2;



		for(int x = 0; x < width;x++) {
			
			for(int z = 0; z < length;z++) {
				Ray ray = new Ray(transform.position + new Vector3(x * spawnDistance + xOffset, 0, z * spawnDistance + zOffset),Vector3.down);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit, raycastRange)) {

					GameObject newNPC = uLink.Network.Instantiate(proxyPrefab,creatorPrefab,hit.point,Quaternion.Euler(Vector3.zero),0,"") as GameObject;

					SceneManager.Instance.actors.Add (newNPC.GetComponent<Actor>());
					newNPC.GetComponent<AIController>().currentGrid = grid;
				}
			}
		}

	}

}
