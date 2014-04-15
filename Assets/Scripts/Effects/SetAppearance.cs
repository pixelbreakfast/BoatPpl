using UnityEngine;
using System.Collections;
using System.IO;

public class SetAppearance : uLink.MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		if(uLink.Network.isServer) {

			Object[] textures = Resources.LoadAll("Textures/HeadTextures");
			
			Texture2D texture = textures[Random.Range(0, textures.Length)] as Texture2D;

			Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			if(texture == null) {
				Debug.Log ("another one here");

			}

			networkView.RPC ("RPCSetAppearance", uLink.RPCMode.AllBuffered, texture.name, color);

		}
	}

	[RPC]
	public void RPCSetAppearance(string textureName, Color color) {
		Object[] textures = Resources.LoadAll("Textures");
		
		Texture2D texture = Resources.Load("Textures/HeadTextures/" + textureName) as Texture2D;

		Texture2D normal = Resources.Load("Normals/" + textureName) as Texture2D;


		TraverseAndSetAppearance(gameObject, texture, normal, color);
	}

	public void TraverseAndSetAppearance (GameObject obj, Texture2D texture, Texture2D normal, Color color) 
	{

		foreach (Transform child in obj.transform)
		{
			TraverseAndSetAppearance(child.gameObject, texture, normal, color);
			if(child.renderer != null) {
				if(child.tag == "Body") {
					child.renderer.material.color = color;
					
				}
				if(child.tag == "Head") {
					child.renderer.material.mainTexture = texture;
					child.renderer.material.SetTexture("_BumpMap", normal);
					
				}
			}
		}


	}


}
