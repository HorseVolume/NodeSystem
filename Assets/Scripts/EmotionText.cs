using UnityEngine;
using System.Collections;

public class EmotionText : MonoBehaviour {

	public Color color;
	public GameObject user;

	// Update is called once per frame
	void Update () {
		TextMesh textMesh = GetComponent<TextMesh> ();
		textMesh.color = color;
		Color userColor = color;
		userColor.a = 1;
		user.GetComponent<Renderer> ().material.SetColor ("_color", color);





	}
}
