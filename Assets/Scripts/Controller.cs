using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//peek circle
	public GameObject peekCircle;

	//mouselook
	public GameObject mainCamera;
	public GameObject user;
	public Transform angleTarget;


	private Vector3 mousePosition;


	//user rotation

	public float rotationSpeed;

	private Vector3 userDirection;
	private float userAngle;

	private Vector3 userTargetDirection;
	private float userTargetAngle;
	private Quaternion userRotation;


	//targets
	public float targetingWiggleRoom;
	private GameObject[] targets;
	private float[] userTargetNodeAngles;
	private GameObject[] userTargetNodeTargets;
	private Vector3 userTargetNodeDirection;


	// Use this for initialization
	void Start () {
		//fill target array
		targets = GameObject.FindGameObjectsWithTag ("Node");

		//start user on START node
		if (targets.Length > 0) {
			GetTargetAngles();
		}

	
	}
	
	// Update is called once per frame
	void Update () {

		//move camera
		mousePosition = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
		mainCamera.transform.position = new Vector3 ((user.transform.position.x + mousePosition.x) / 2, (user.transform.position.y + mousePosition.y) / 2, mainCamera.transform.position.z);


		//determine angle of user
		userTargetDirection = user.transform.position - mousePosition;
		userDirection = user.transform.position - angleTarget.position;
		userTargetAngle = Mathf.Atan2(userTargetDirection.y, userTargetDirection.x) * Mathf.Rad2Deg;
		//rotate user to mouse position
		userRotation = Quaternion.AngleAxis (userTargetAngle, Vector3.forward);
		user.transform.rotation = Quaternion.Slerp (user.transform.rotation, userRotation, Time.deltaTime * rotationSpeed);

		//targets
		userAngle = Mathf.Atan2(userDirection.y, userDirection.x) * Mathf.Rad2Deg;
		for (int i = 0; i < userTargetNodeAngles.Length; i++) {
			if (userTargetNodeAngles[i] != 0){
				//Debug.Log(userTargetNodeAngles[i] + " " + userAngle);
				if (userAngle < (userTargetNodeAngles[i] + targetingWiggleRoom) && userAngle > (userTargetNodeAngles[i] - targetingWiggleRoom)){
					//hilighting
					peekCircle.transform.position = userTargetNodeTargets[i].transform.position;
					peekCircle.SetActive(true);
					peekCircle.ScaleTo(300, Vector3(6,6,6), 0);
				} else {
					peekCircle.SetActive(false);
				}
			}

		}

		
	}

	void GetTargetAngles() {
		for (int i = 0; i < targets.Length; i++) {
			Node currentNode = targets[i].GetComponent<Node> ();
			if (currentNode.isActiveNode == true){
				user.transform.position = targets[i].transform.position;
				userTargetNodeAngles = new float[currentNode.targets.Length];
				userTargetNodeTargets = new GameObject[currentNode.targets.Length] ;
				userTargetNodeTargets = currentNode.targets;


				for (int k = 0; k < userTargetNodeAngles.Length; k++){
					//userTargetNodeAngles[k] = currentNode.targets[k].transform.position;

					if (currentNode.targets[k]) {
						userTargetNodeDirection = user.transform.position - currentNode.targets[k].transform.position;
						//Debug.Log (currentNode.targets[k].transform.position);
						userTargetNodeAngles[k] = Mathf.Atan2(userTargetNodeDirection.y, userTargetNodeDirection.x) * Mathf.Rad2Deg;
						//Debug.Log (userTargetNodeAngles[k]);
						//Debug.Log (userTargetNodeAngles);
					}
					
					
				}
			}
		}

	}
}
