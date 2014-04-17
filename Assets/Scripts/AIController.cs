using UnityEngine;
using System.Collections;


enum CharacterAction {
	Walk,
	Idle
}

public class AIController : ActorController {

	public static Vector3 GetNewDestination() {

		int x = Random.Range (-3, 3);
		int z = Random.Range (-3, 3);
		Vector3 newPosition = new Vector3(x, 0, z);

		return newPosition;
	}

	public Node destinationNode;
	public Grid currentGrid;
	public float searchRange = 0.8f;

	float actionInterval;


	public Vector3 spriteOffset;
	
	
	float destinationThreshhold = 0.2f;


	// Use this for initialization
	void Start () {
	
		actionInterval = Random.Range (0.1f, 1.0f);
		if(destinationNode == null) {

			float distance = 1000;
			foreach(Node node in currentGrid.nodes) {
				if(Vector3.Distance(transform.position, node.transform.position) < distance) {
					distance = Vector3.Distance(transform.position, node.transform.position);
					destinationNode = node;
				}
			}
		}
		InvokeRepeating ("UpdateAction", actionInterval, actionInterval);
	}
	
	// Update is called once per frame
	void Update () {


		if(destinationNode == null) return;


			float distance = Vector3.Distance(destinationNode.transform.position, transform.position);
			
			if(distance < destinationThreshhold) 
			{
				//SetAction(CharacterAction.Idle);

			} 
			else 
			{
				
			Vector3 normal = Vector3.Normalize( destinationNode.transform.position - transform.position);
			Vector3 forward = new Vector3(normal.x, 0, normal.z);
			if(forward != Vector3.zero) {
				transform.forward = forward;
			}
				
				Move (normal);

				
			}
			
			
	}

	
	Vector3 selectDestination() {

		return GetNewDestination();
	}
	
	void SetAction(CharacterAction npcAction) {
	
	}

	void UpdateAction() {

		if(currentGrid == null) return;
		
			Node newDestination = GetNearestNode();
			foreach(Node node in currentGrid.nodes) 
			{
				
				if(GetNodeUndesirability(node) < GetNodeUndesirability(newDestination)) {
					newDestination = node;
					
					
				}
				
			}

			destinationNode = newDestination;

		

	}

	Node GetNearestNode () {
		float nearestDistance = 10000;
		Node nearestNode = null;
		foreach(Node node in currentGrid.nodes) {

				float distance = Vector3.Distance(transform.position, node.transform.position);
				if(distance < nearestDistance) {
					nearestDistance = distance;
					nearestNode = node;
				}

		}
		return nearestNode;
	}


	float GetNodeUndesirability(Node node) {
		float undesirability = node.occupancy;

		undesirability += Vector3.Distance(transform.position, node.transform.position);
		return undesirability;
	}

}
