using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Health health;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 25), health.health.ToString());
	}
}
