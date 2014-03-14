using UnityEngine;
using System.Collections;

public class Wave: MonoBehaviour
{

	public float moveAmount = 1;
	Vector3 startPosition;

	// Use this for initialization
	void Start ()
	{
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 newPosition = startPosition + new Vector3(Mathf.Sin(Time.realtimeSinceStartup) * moveAmount, 0, 0);
		transform.position = newPosition;
	}
}
