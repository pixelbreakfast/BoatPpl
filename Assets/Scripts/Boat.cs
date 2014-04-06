using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Messenger.AddListener("docked", Detonate);
		
	}
	
	// Update is called once per frame
	void Detonate () {
		Camera.main.transform.parent = null;
		GameObject.Instantiate(Resources.Load ("Exploding Boat"),transform.position,transform.rotation);
		Destroy (gameObject);
	}
}
