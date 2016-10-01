using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineRenderer : MonoBehaviour {


	public GameObject lineDrawPrefabs = null; // this is where we put the prefabs object

	private bool isMousePressed;
	private GameObject lineDrawPrefab;
	private LineRenderer lineRenderer;
	private List<Vector3> drawPoints = new List<Vector3>();
	private Gradient g = new Gradient ();


	// Use this for initialization
	void Start () {
		isMousePressed = false;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(1))
		{
			// delete the LineRenderers when right mouse down
			GameObject [] delete = GameObject.FindGameObjectsWithTag("LineDraw");
			int deleteCount = delete.Length;
			for(int i = deleteCount - 1; i >= 0; i--)
				Destroy(delete[i]);
		}


		if(Input.GetMouseButtonDown(0))
		{
			// left mouse down, make a new line renderer
			isMousePressed = true;
			setUpLine ("line",0.2f);

		}
		else if(Input.GetMouseButtonUp(0))
		{
			// left mouse up, stop drawing
			isMousePressed = false;
		}

		if(isMousePressed)
		{

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//print (ray.origin);

			Vector3 mousePos = ray.origin;
			//print (mousePos);

			if (!drawPoints.Contains (mousePos)) 
			{
				drawPoints.Add (mousePos);
				DrawLine (drawPoints,mousePos);
			}
		}
		if (!isMousePressed) {
			drawPoints.Clear ();
		}




	}

	void setUpLine(string lineName,float width){

		lineDrawPrefab = GameObject.Instantiate(lineDrawPrefabs) as GameObject;
		lineDrawPrefab.name = lineName;
		lineRenderer = lineDrawPrefab.GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));

		lineRenderer.SetColors (Color.black,ExtensionMethods.RandomColor());
		lineRenderer.SetWidth(width, width);
		lineRenderer.SetVertexCount(0);


	}
	void DrawLine(List<Vector3> drawPoints, Vector3 mousePosition)
	{
		
		lineRenderer.SetVertexCount (drawPoints.Count);
		lineRenderer.SetPosition(drawPoints.Count - 1, mousePosition);

	}

//	Color colors(Color color1, Color color2, Color color3, Color color4, float time){
//
//
//		Color[] colors = new Color[]{};
//		GradientColorKey[] gradientColorKey = new GradientColorKey[] {
//			new GradientColorKey (color1, 0f), 
//			new GradientColorKey (color2, 0.35f), 
//			new GradientColorKey (color3, 0.7f),
//			new GradientColorKey (color4, 1f)
//		};
//		GradientAlphaKey[] gradientAlphaKey = new GradientAlphaKey[] {
//
//			new GradientAlphaKey (1, 0f),
//			new GradientAlphaKey (1, 0.35f),
//			new GradientAlphaKey (1, 0.7f),
//			new GradientAlphaKey (1, 1)
//		};
//
//		g.SetKeys(gradientColorKey, gradientAlphaKey);
//
//		return Color32.Lerp(g.Evaluate(0), g.Evaluate(1), time);
//
//	}


}