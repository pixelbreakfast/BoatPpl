using uLink;
using UnityEngine;
using System.Collections.Generic;

public class AuthSpawnPrefab : uLink.MonoBehaviour
{

	public Grid grid;
	public GameObject npcProxyPrefab;
	public GameObject npcCreatorPrefab;
	public float numberOfNPCs = 20;
	public float spawnRate;

	//************************************************************************************************
	// Owner is the actual player using the client Scene. It has animations + camera.  
	// OwnerInit.cs connects the owner to the camera.
	//
	// Proxy is what appears on the the opponent players computers, It has animations but no camera connection.
	//
	// Creator is instansiated in the server and it has no camera. It has the animation script, but it has been 
	// deactivated.  Just turn it on if you really do want to see animations on the server.
	//************************************************************************************************
	
	public GameObject proxyPrefab = null;
	public GameObject ownerPrefab = null;
	public GameObject creatorPrefab = null;
	GameObject[] spawnLocations;
	int spawnLocationIndex = -1;
	GameObject spawnLocation;
	List<uLink.NetworkPlayer> queuedPlayers = new List<uLink.NetworkPlayer>();

	public void Start() {
		spawnRate = GameManager.Instance.timeUntilVoyage / numberOfNPCs;



		Messenger.AddListener("start_game", StartGame);
		Messenger.AddListener("start_voyage", CancelSpawn);
	}

	public void StartGame () {
		spawnLocations = GameObject.FindGameObjectsWithTag("Spawn");

		if(spawnLocations == null) {
			Debug.LogError("No GameObjects tagged as 'Spawn.'  Tag a GameObject as 'Spawn'");
		} else {
			spawnLocation = GetNextSpawnLocation();
		}

		GameObject newActor = uLink.Network.Instantiate(proxyPrefab,ownerPrefab,spawnLocation.transform.position,spawnLocation.transform.rotation,0,"") as GameObject;
		newActor.AddComponent<Health>();
		GameManager.Instance.actors.Add(newActor.GetComponent<Actor>());

		InvokeRepeating("SpawnNextActor",spawnRate,spawnRate);
	}
		
	public void CancelSpawn() 
	{
		CancelInvoke("SpawnNextActor");
	}

	void uLink_OnPlayerConnected(uLink.NetworkPlayer player)
	{
		queuedPlayers.Add(player);
	
	}
	
	void SpawnNextActor() {
		if(GameManager.Instance.actors.Count < numberOfNPCs) {	
			GameObject newActor;

			if(queuedPlayers.Count <= 0) {

				newActor = uLink.Network.Instantiate(npcProxyPrefab,npcCreatorPrefab,spawnLocation.transform.position,spawnLocation.transform.rotation,0,"") as GameObject;

				newActor.GetComponent<AIController>().currentGrid = grid;
				

			} else {

				uLink.NetworkPlayer player = queuedPlayers[0];
				queuedPlayers.RemoveAt (0);

				string loginName;
				if (!player.loginData.TryRead<string>(out loginName)) loginName = "Nameless";
				
				//Instantiates an avatar for the player connecting to the server
				//The player will be the "owner" of this object. Read the manual chapter 7 for more
				//info about object roles: Creator, Owner and Proxy.
				newActor = uLink.Network.Instantiate(player, proxyPrefab, ownerPrefab, creatorPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation, 0, loginName);

			}

			GameManager.Instance.actors.Add(newActor.GetComponent<Actor>());

			spawnLocation = GetNextSpawnLocation();

		}
	}

	GameObject GetNextSpawnLocation() {

		spawnLocationIndex++;
		
		if(spawnLocationIndex == spawnLocations.Length) {
			spawnLocationIndex = 0;
		}

		return spawnLocations[spawnLocationIndex];
	}
}