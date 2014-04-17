using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {


	float growFrequency = 0.1f;
	float growAmount = 0.002f;
	// Use this for initialization
	void Start () {

		InvokeRepeating("IncreaseSize", 0, growFrequency);
	}
	
	// Update is called once per frame
	void IncreaseSize () {
		transform.localScale += new Vector3(growAmount,growAmount, growAmount);
 
	}
}
