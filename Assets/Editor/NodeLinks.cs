using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]

public class NodeLink : Editor {


	private GameObject[] nodeList;
//	private int nodeCount;
	private Vector3[] elementList;
	private GameObject currentNode;
	private Vector3 currentNodePosition;
	private GameObject[] nodeTargetList;

	private void OnSceneGUI () {
		Node node = target as Node;
		nodeList = GameObject.FindGameObjectsWithTag ("Node");
		//Debug.Log (nodeList.Length);
//		nodeCount = nodeList.Length;


		Handles.color = Color.red;
		//Debug.Log(nodeList.Length);
		//for every node 

		for (int i = 0; i < nodeList.Length; i++) {
			//make next node a target
			Node nodeTarget = nodeList[i].GetComponent<Node> ();
			//nodeTargetList = nodeTarget.targets;
			//make current node posiition the current target
			currentNodePosition = nodeTarget.transform.position;
			Handles.Label (currentNodePosition + Vector3.down + Vector3.left , nodeTarget.name);
			//Debug.Log(currentNodePosition);
				//if the current target has targets
				if (nodeTarget.targets[0]){
					//for each of the target's targets
					for (int k = 0; k < nodeTarget.targets.Length; k++) {
						//if selected array position has a value
						if (nodeTarget.targets[k]){
						//Debug.Log();
							//draw a line from the current target to its target position
							Handles.DrawDottedLine(currentNodePosition, nodeTarget.targets[k].transform.position, 5);
							
					}
				}

			}

		}
		currentNodePosition = node.transform.position;
		Handles.color = Color.green;
		for (int i = 0; i < node.targets.Length; i++) {
			if (node.targets[i]){
				Handles.DrawLine(currentNodePosition, node.targets[i].transform.position);
			}
		}

		

	}
}