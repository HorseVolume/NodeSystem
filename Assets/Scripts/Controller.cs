using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//peek circle
	public GameObject peekCirlce;

	//mouselook
	public GameObject mainCamera;
	public GameObject user;

	private Vector3 mousePosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);

		mainCamera.transform.position = new Vector3 ((user.transform.position.x + mousePosition.x) / 2, (user.transform.position.y + mousePosition.y) / 2, mainCamera.transform.position.z);


	}
}
