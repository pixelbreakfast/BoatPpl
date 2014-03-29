using UnityEngine;
using System.Collections;

public class TimedRemoval : MonoBehaviour {

	public float delay;

	// Use this for initialization
	void Start () {
		StartCoroutine("Remove");
	}
	
	IEnumerator Remove() {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
