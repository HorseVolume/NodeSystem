using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Vector3 p0;
	public Vector3 p1;
	private LineRenderer lineRenderer;
	public bool isEnabled;
	public float width;
	public Color activeColor;
	public Color inactiveColor;
	private Color c;
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		
		//color link according to status

		if (isEnabled == true) {
			c = activeColor;
		} else {
			c = inactiveColor;
		}
		//c.a = 0.5f;
		
		//draw line
		///lineRenderer.material = new Material ("Line");
		//ineRenderer.material.SetColor ("_Color", c);
		//lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));


		Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Color"));
		lineRenderer.material = whiteDiffuseMat;

		lineRenderer.SetWidth(width, width);
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, p0);
		lineRenderer.SetPosition(1, p1);

	}

	void Update(){
		lineRenderer.SetColors (c, c);
		lineRenderer.material.color = c;
	}

	void BecomeActive() {
		lineRenderer.material.SetColor ("_Color", Color.green);
	}
	
	void BecomeInactive() {
		lineRenderer.material.SetColor ("_Color", Color.red);
	}

}
 