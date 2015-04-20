using UnityEngine;
using System.Collections;

public class NodeUser : MonoBehaviour {

	public TextMesh angle;
	public GameObject angleTarget;
	public Vector3 normalizedHeading;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		normalizedHeading = angleTarget.transform.position - transform.position;
		angle.text = "Angle: " + normalizedHeading;

	}
}
