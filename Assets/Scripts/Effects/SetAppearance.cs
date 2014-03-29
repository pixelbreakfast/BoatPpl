using UnityEngine;
using System.Collections;
using System.IO;

public class SetAppearance : MonoBehaviour {
	protected Texture2D texture;
	protected Texture2D normal; 

	// Use this for initialization
	void Start () {
		Object[] textures = Resources.LoadAll("Textures");
		texture = textures[Random.Range(0, textures.Length)] as Texture2D;
		normal = Resources.Load("Textures/Normals/" + texture.name) as Texture2D;

		TraverseAndSetAppearance(gameObject);
	}

	public void TraverseAndSetAppearance (GameObject obj) {


		foreach (Transform child in obj.transform)
		{
			TraverseAndSetAppearance(child.gameObject);
			if(child.renderer != null) {
				if(child.tag == "Body") {
					child.renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
					
				}
				if(child.tag == "Head") {
					child.renderer.material.mainTexture = texture;
					child.renderer.material.SetTexture("_BumpMap", normal);
					
				}
			}
		}


	}
}
