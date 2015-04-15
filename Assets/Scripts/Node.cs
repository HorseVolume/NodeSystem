using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public GameObject[] targets;
	public Vector3[] targetHeadings;
	public bool isActiveNode;
	//public TextMesh angle;
	private GameObject[] links;

	public void Awake() {


		if (targets.Length > 0 && targets[0]) {

			targetHeadings = new Vector3[targets.Length];
			links = new GameObject[targets.Length];

			for (int i = 0; i < targets.Length; i++){


				if (targets[i]){
					targetHeadings[i] = targets[i].transform.position - transform.position;
					targetHeadings[i].Normalize();

					//draw links
					GameObject linkClone = Instantiate(Resources.Load("Link")) as GameObject;
					linkClone.GetComponent<Link>().p0 = transform.position;
					linkClone.GetComponent<Link>().p1 = targets[i].transform.position;
					links[i] = linkClone;

				}
			}
		}
	}

	public void Start() {
		//angle.text = "Angle: " + targetHeadings[0];
	}


}