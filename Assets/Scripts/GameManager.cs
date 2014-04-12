using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : uLink.MonoBehaviour {

	// Static singleton property
	public static GameManager Instance { get; private set; }
	public List<Actor> actors;

	public int time = 0;
	public float timeUntilVoyage;
	public float voyageLength;
	public float timeUntilDocked;
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
				BroadcastMessage("start_game");
				showGUI = false;
				//Camera.main.gameObject.AddComponent<InspectionCamera>();
				//Camera.main.gameObject.AddComponent<MouseLook>();
			}
		}

	}

	void BroadcastMessage(string message) {

		Messenger.Broadcast(message);
	
		networkView.RPC("RebroadcastMessage", uLink.RPCMode.Others, message);

	}
	// Use this for initialization
	void Start() {

	}


	// Update is called once per frame
	void UpdateTimer () 
	{
		time++;

		if(time == timeUntilVoyage) {
			BroadcastMessage("start_voyage");
		}

		if(time == voyageLength + timeUntilVoyage) {
			BroadcastMessage("end_voyage");
		}

		if(time == voyageLength + timeUntilVoyage + timeUntilDocked) {
			BroadcastMessage("docked" );

			List<Actor> actors = new List<Actor>();

			foreach(Actor actor in GameManager.Instance.actors) {
				actors.Add(actor);
			}

			foreach(Actor actor in actors) {
				actor.Die();
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
