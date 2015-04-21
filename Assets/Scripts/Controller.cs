using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//peek circle
	public GameObject peekCircle;
	public TextMesh peekText;
	public Vector3 scaleTo;
	public Vector3 scaleFrom;
	public float scaleTime;
	private Animator peekAnimator;
	private bool peekIsActive;


	//mouselook
	public bool mouseLookEnabled;
	public GameObject mainCamera;
	public GameObject user;
	public Transform angleTarget;

	//HintText
	public Vector3 hintTextScale;
	public Vector3 previousHintTextScale;
	public float hintSpeed;

	//Emotions
	public Color angerColor;
	public Color compassionColor;
	public Color suspicionColor;
	public GameObject emotionText;


	public float angerStrength;
	public float compassionStrength;
	public float suspicionStrength;

	private int currentEmotion = 0;
	private int prevEmotion;


	private Vector3 mousePosition;



	//Clicking
	private Vector3 clickPoint;
	
	//private float outlineWidth = 10f;
//	private int  prevOutlineWidth = 10;
	
	//private bool infoBox = false;
	//private bool pointers = false;
	//private bool outline = false;
	
	private Vector2 userScreenPos;


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
		peekAnimator = peekCircle.GetComponent<Animator> ();

		//start user on START node
		if (targets.Length > 0) {
			GetTargetAngles();
		}



		mainCamera.transform.position = new Vector3 (user.transform.position.x, user.transform.position.y, mainCamera.transform.position.z);




	
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);

		emotionText.transform.position = new Vector3(user.transform.position.x - 4, user.transform.position.y - 1, user.transform.position.z);


		//move camera
		if (mouseLookEnabled == true) {



		}
	



		//prevEmotion = currentEmotion;
		

		if (Input.GetMouseButtonDown (0)) {
			clickPoint = mousePosition;
		}

		if (Input.GetKeyDown ("left")) {

			if (currentEmotion == 2){
				currentEmotion = 1;
			} else if (currentEmotion == 1){
				currentEmotion = 0;
			} else if (currentEmotion == 0){
				currentEmotion = 2;
			}

		} else if (Input.GetKeyDown ("right")) {
			if (currentEmotion == 2){
				currentEmotion = 0;
			} else if (currentEmotion == 1){
				currentEmotion = 2;
			} else if (currentEmotion == 0){
				currentEmotion = 1;
			}

		} else {
			//mouselook
			mainCamera.transform.position = new Vector3 ((user.transform.position.x + mousePosition.x) / 2, (user.transform.position.y + mousePosition.y) / 2, mainCamera.transform.position.z);
			//move charty
			userScreenPos = mainCamera.GetComponent<Camera> ().WorldToScreenPoint (user.transform.position);



			//rotation user stuff

			//determine angle of user
			userTargetDirection = user.transform.position - mousePosition;
			userDirection = user.transform.position - angleTarget.position;
			userTargetAngle = Mathf.Atan2(userTargetDirection.y, userTargetDirection.x) * Mathf.Rad2Deg;
			//rotate user to mouse position
			userRotation = Quaternion.AngleAxis (userTargetAngle, Vector3.forward);
			user.transform.rotation = Quaternion.Slerp (user.transform.rotation, userRotation, Time.deltaTime * rotationSpeed);
		}

		if (currentEmotion == 0) {
			emotionText.GetComponent<TextMesh> ().text = "Anger";
			emotionText.GetComponent<Animator> ().SetInteger("emotion", 0);
		} else if (currentEmotion == 1) {
			emotionText.GetComponent<TextMesh> ().text = "Compassion";
			emotionText.GetComponent<Animator> ().SetInteger("emotion", 1);
		} else if (currentEmotion == 2) {
			emotionText.GetComponent<TextMesh> ().text = "Suspicion";
			emotionText.GetComponent<Animator> ().SetInteger("emotion", 2);
		}
	

		

		//Emotion Changing

	







		//targets

		//angle user
		userAngle = Mathf.Atan2(userDirection.y, userDirection.x) * Mathf.Rad2Deg;

		peekIsActive = false;

		for (int i = 0; i < userTargetNodeAngles.Length; i++) {
			if (userTargetNodeAngles[i] != 0){
				//Debug.Log(userTargetNodeAngles[i] + " " + userAngle);
				if (userAngle < (userTargetNodeAngles[i] + targetingWiggleRoom) && userAngle > (userTargetNodeAngles[i] - targetingWiggleRoom)){
					//hilighting
					peekCircle.transform.position = userTargetNodeTargets[i].transform.position + Vector3.back;
					peekText.transform.position = peekCircle.transform.position;
					//peekCircle.SetActive(true);
					//iTween.ScaleTo(peekCircle, scaleTo ,scaleTime);
					//peekCircle.ScaleTo(300, Vector3(6,6,6), 0);
					//set text
					peekText.text = userTargetNodeTargets[i].GetComponent<Node>().previewText[0];

					peekIsActive = true;
					GameObject hintTextTemp = userTargetNodeTargets[i].transform.GetChild(0).gameObject;
					Vector3 screenPosTemp = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 6));
					iTween.MoveUpdate(hintTextTemp,screenPosTemp, hintSpeed);
					iTween.ScaleUpdate(hintTextTemp, hintTextScale, hintSpeed);



				} else {
					//if (userTargetNodeTargets[i].transform.GetChild(0).transform.position != userTargetNodeTargets[i].transform.position) {
					if (userTargetNodeTargets[i].transform.GetChild(0).gameObject) {
						GameObject hintTextTemp2 = userTargetNodeTargets[i].transform.GetChild(0).gameObject;
						Vector3 parentTemp2 = userTargetNodeTargets[i].transform.position;
						parentTemp2 = new Vector3(parentTemp2.x - 1, parentTemp2.y + 2, parentTemp2.z);
						iTween.MoveUpdate(hintTextTemp2,parentTemp2, hintSpeed);
						iTween.ScaleUpdate(hintTextTemp2, previousHintTextScale, hintSpeed);
					}


					
					//}
				}
			}

		}

		if (peekCircle.transform.localScale.x <= 0 && peekIsActive == true) {
			peekAnimator.SetBool ("open", true);
			//peekText.text = userTargetNodeTargets[i].GetComponent<Node>().previewText[0];
		} else if (peekCircle.transform.localScale.x > 0 && peekIsActive == false) {
			peekAnimator.SetBool ("open", false);
			peekText.text = "";
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
