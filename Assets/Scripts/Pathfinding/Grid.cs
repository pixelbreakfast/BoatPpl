using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	
	public int width = 30;
	public int length = 30;
	float raycastRange = 100;
	float xOffset;
	float zOffset;

	float debugSphereSize = 1f;

	public float gridSpacing;
	public List<Node> nodes;

	public LayerMask environmentLayerMask;
	public bool debugMode;

	// Use this for initialization
	void Start () {
		xOffset = width/2;
		zOffset = length/2;

		environmentLayerMask = 1 << LayerMask.NameToLayer("Walkable");

		if(nodes == null) nodes = new List<Node>();

		for(int x = 0; x < width;x++) {
			
			for(int z = 0; z < length;z++) {

				Ray ray = new Ray(transform.position + new Vector3(x * gridSpacing, 0, z * gridSpacing),Vector3.down);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit, raycastRange, environmentLayerMask)) {

					if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Walkable")) {


						GameObject nodeGameObject;
						if(debugMode) {
							nodeGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
							Destroy(nodeGameObject.GetComponent<Collider>());
							nodeGameObject.renderer.material.color = new Color(1, 1, 1);

						} else {
							nodeGameObject = new GameObject();
						}

						nodeGameObject.name = "Node";
						Node newNode = nodeGameObject.AddComponent<Node>() as Node;
						newNode.transform.position = hit.point;
						newNode.transform.parent = hit.transform;
						newNode.transform.localScale = newNode.transform.localScale * debugSphereSize + new Vector3(0.1f,0.1f,0.1f);
						nodes.Add(newNode);
					}

				}


			}
		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
