using UnityEngine;
using System.Collections;

public class LoadLevelButton : MonoBehaviour
{
	public string destination;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(destination);
		}
	}
}