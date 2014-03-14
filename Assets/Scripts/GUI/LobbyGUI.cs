using UnityEngine;
using System.Collections;

public class LobbyGUI : MonoBehaviour {

	new public NetworkView networkView;
	
	public string selectedLevel = "Pathfinding";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnGUI () {
		if(GUI.Button(new Rect(100, 100, 200, 50),"Start Game")) {
			//Application.LoadLevel(selectedLevel);
			networkView.RPC("LoadLevel", RPCMode.All,selectedLevel); 
		}
	}
}
