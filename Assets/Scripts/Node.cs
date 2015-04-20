using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public string hint;
	[Multiline]
	public string[] previewText;
	private TextMesh hintText;

	public GameObject[] targets;
	public Vector3[] targetHeadings;
	public bool isActiveNode;
	//public TextMesh angle;
	private GameObject[] links;

	public void Awake() {

		if (transform.childCount > 0) {
			hintText = transform.GetChild (0).GetComponent<TextMesh> ();
			hintText.text = hint;
			Vector3 parentTemp2 = this.transform.position;
			parentTemp2 = new Vector3(parentTemp2.x - 1, parentTemp2.y + 2, parentTemp2.z);
			hintText.transform.position = parentTemp2;


		}



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