using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : uLink.MonoBehaviour {

	// Static singleton property
	public static GameManager Instance { get; private set; }
	public List<Actor> actors;

	public int time = 0;
	public int timeUntilVoyage;
	public float voyageLength;
	bool showGUI = true;
	
	void Awake()
	{	
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}
		
		// Here we save our singleton instance
		Instance = this;
	}


	void OnGUI() 
	{
		GUI.Label(new Rect(50,50,100, 25), time.ToString());

		if(showGUI) {

			if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Start Game")) {
				InvokeRepeating("UpdateTimer", 1, 1);
				Messenger.Broadcast("start_game");
				showGUI = false;
				Camera.main.gameObject.AddComponent<InspectionCamera>();
				Camera.main.gameObject.AddComponent<MouseLook>();
			}
		}

	}

	// Use this for initialization

	// Update is called once per frame
	void UpdateTimer () 
	{
		time++;

		if(time == timeUntilVoyage) {
			Debug.Log("Begin Voyage");
			foreach(Actor actor in GameManager.Instance.actors) {
				actor.networkView.RPC("StartVoyage", uLink.RPCMode.All);
			}
		}

		if(time >= voyageLength + timeUntilVoyage) {
			Debug.Log("Game Over");
			foreach(Actor actor in GameManager.Instance.actors) {
				actor.networkView.RPC ("Win", uLink.RPCMode.All);
			}
			CancelInvoke("UpdateTimer");
		}

	}

	public void OnPlayerConnected (NetworkPlayer newPlayer) {
		UnityEngine.Object[] characterControllers = Object.FindObjectsOfType (typeof(CharacterController));
		
		foreach(CharacterController characterControllerA in characterControllers)
		{
			foreach(CharacterController characterControllerB in characterControllers) 
			{
				if(characterControllerA != characterControllerB) {
					characterControllerA.gameObject.SetActive(true);
					characterControllerB.gameObject.SetActive(true);
					Physics.IgnoreCollision(characterControllerA.collider, characterControllerB.collider);
				}
				
			}
			
		}
		

	}
}
