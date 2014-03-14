using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Traverse(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void Traverse(GameObject obj)
	{
		
		foreach (Transform child in obj.transform)
		{
			Traverse(child.gameObject);
			if(child.renderer != null) {
				if(child.tag == "ColorMix") {
					child.renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
					
				}
				
			}
		}
		
	}
}
