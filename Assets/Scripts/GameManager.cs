using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : uLink.MonoBehaviour {

	// Static singleton property
	public static GameManager Instance { get; private set; }
	public List<Actor> actors;
	List<uLink.NetworkPlayer> players = new List<uLink.NetworkPlayer>();

	public int time = 0;
	public float timeUntilVoyage;
	public float voyageLength;
	public float timeUntilDocked;
	bool showGUI = true;
	
	private const float WIDTH = 220;

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
		


		//GUI.Label(new Rect(50,50,100, 25), time.ToString());

		if(showGUI) {

			
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			
			GUILayout.BeginVertical("Box", GUILayout.Width(WIDTH));

			GUILayout.BeginVertical();
			GUILayout.Space(5);
			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("Waiting for players. " + players.Count + " have joined.");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			
			GUILayout.BeginVertical();
			GUILayout.Space(5);
			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			foreach(uLink.NetworkPlayer player in players) {
				
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label(player.ToString());
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}

			if (GUILayout.Button("Start Game", GUILayout.Width(120), GUILayout.Height(25)))
			{		
				InvokeRepeating("UpdateTimer", 1, 1);
				BroadcastMessage("start_game");
				showGUI = false;
				//Camera.main.gameObject.AddComponent<InspectionCamera>();
				//Camera.main.gameObject.AddComponent<MouseLook>();
			}
			
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			
			GUILayout.BeginVertical();
			GUILayout.Space(2);
			GUILayout.EndVertical();
			GUILayout.EndVertical();
			
			
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();



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

	void uLink_OnPlayerConnected(uLink.NetworkPlayer newPlayer)
	{

		players.Add(newPlayer);

	}
}
