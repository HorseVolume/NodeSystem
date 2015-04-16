using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//peek circle
	public GameObject peekCirlce;

	//mouselook
	public GameObject mainCamera;
	public GameObject user;

	private Vector3 mousePosition;


	//user rotation

	public float rotationSpeed;

	private Vector3 userDirection;
	private float userAngle;

	private Vector3 userTargetDirection;
	private float userTargetAngle;
	private Quaternion userRotation;


	//targets
	private GameObject[] targets;
	private float[] userTargetNodeAngles;


	// Use this for initialization
	void Start () {
		//fill target array
		targets = GameObject.FindGameObjectsWithTag ("Node");

		//start user on START node
		if (targets.Length > 0) {
			for (int i = 0; i < targets.Length; i++) {
				Node currentNode = targets[i].GetComponent<Node> ();
				if (currentNode.isActiveNode == true){
					user.transform.position = targets[i].transform.position;
					userTargetNodeAngles = new float[currentNode.targets.Length];
					for (int k = 0; k < userTargetNodeAngles.Length; k++){
						//userTargetNodeAngles[k] = currentNode.targets[k].transform.position;
						Vector3 userTargetNodeDirection = user.transform.position - currentNode.targets[k].transform.position;
						userTargetNodeAngles[k] = Mathf.Atan2(userTargetNodeDirection.y, userTargetNodeDirection.x) * Mathf.Rad2Deg;

					}
				}
			}
		}

	
	}
	
	// Update is called once per frame
	void Update () {

		//move camera
		mousePosition = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
		mainCamera.transform.position = new Vector3 ((user.transform.position.x + mousePosition.x) / 2, (user.transform.position.y + mousePosition.y) / 2, mainCamera.transform.position.z);


		//determine angle of user
		userTargetDirection = user.transform.position - mousePosition;
		userTargetAngle = Mathf.Atan2(userTargetDirection.y, userTargetDirection.x) * Mathf.Rad2Deg;
		//rotate user to mouse position
		userRotation = Quaternion.AngleAxis (userTargetAngle, Vector3.forward);
		user.transform.rotation = Quaternion.Slerp (user.transform.rotation, userRotation, Time.deltaTime * rotationSpeed);

		//targets loop

		
	}
}
