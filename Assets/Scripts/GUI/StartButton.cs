using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{

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
		//guiText.font.material.color = Color.white;

		if(Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("Boat");
		}
	}
}