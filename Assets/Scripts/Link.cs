using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Vector3 p0;
	public Vector3 p1;
	private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		lineRenderer.material.SetColor ("_Color", Color.red);
		lineRenderer.SetWidth(0.3f, 0.3f);
		lineRenderer.SetVertexCount(2);

		lineRenderer.SetPosition (0, p0);
		lineRenderer.SetPosition (1, p1);
	}

	void BecomeActive() {
		lineRenderer.material.SetColor ("_Color", Color.green);
	}
	
	void BecomeInactive() {
		lineRenderer.material.SetColor ("_Color", Color.red);
	}

}
 