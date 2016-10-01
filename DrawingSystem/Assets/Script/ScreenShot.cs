using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

	private Texture2D screenCap;
	private Texture2D border;

	private Rect captureFrame;

	private Vector2 top;
	private Vector2 bottom;
	private Vector2 left;
	private Vector2 right;

	private int strokeLength = 2;
	private int width = 300;
	private int height = 200;

	private bool shot = false;

	public enum CaptureType { border, screen };
	public CaptureType captureType = CaptureType.border;

	[Range(10f,50f)] public float borderSpeed = 10f;

	void setBorder(){


		switch (captureType) {

		case CaptureType.border:  
			screenCap = new Texture2D( width, height, TextureFormat.RGB24, false);  

			border = new Texture2D(2, 2, TextureFormat.ARGB32, false);
			border.name = "border";
			border.Apply ();

			top = new Vector2 (200, 100);
			bottom = new Vector2 (200, 300);
			left = new Vector2 (200, 100);
			right = new Vector2 (500, 100);

			break;	
		case CaptureType.screen: 
			screenCap = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false);
			break;
		}



	}


	void OnGUI(){

		switch (captureType) {

		case CaptureType.border:  
			

			Vector2 move = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));


			top += move * borderSpeed * Time.deltaTime;
			bottom += move * borderSpeed * Time.deltaTime;
			left += move * borderSpeed * Time.deltaTime;
			right += move * borderSpeed * Time.deltaTime;


			GUI.DrawTexture (new Rect (top, new Vector2 (width, strokeLength)), border, ScaleMode.StretchToFill); //top
			GUI.DrawTexture (new Rect (bottom, new Vector2 (width, strokeLength)), border, ScaleMode.StretchToFill); //bottom
			GUI.DrawTexture (new Rect (left, new Vector2 (strokeLength, height)), border, ScaleMode.StretchToFill); //left
			GUI.DrawTexture (new Rect (right, new Vector2 (strokeLength, height)), border, ScaleMode.StretchToFill); //right

			break;
		}

		//GUI.DrawTexture(new Rect( 0, 0, 1267, 5), border, ScaleMode.StretchToFill); //check screen width
		//GUI.DrawTexture(new Rect( 0, 0, 5, 663), border, ScaleMode.StretchToFill); //check screen height

		if (shot) {

			GUI.DrawTexture(new Rect( 10, 10, 100, 100), screenCap, ScaleMode.StretchToFill); //result
		}
	}


	IEnumerator capture(){

		yield return new WaitForEndOfFrame ();

		switch (captureType) {

		case CaptureType.border:  
			captureFrame = new Rect (bottom.x +1, bottom.y+ 62, width, height  ); 
			break;
		case CaptureType.screen: 
			captureFrame = new Rect (0, 0, Screen.width, Screen.height);
			break;
		}

		screenCap.ReadPixels(captureFrame,0,0,true);
		screenCap.Apply();

		shot = true;

		GameObject.Find ("Cube").GetComponent<MeshRenderer> ().material.mainTexture = screenCap;
		GameObject.Find ("Plane").GetComponent<MeshRenderer> ().material.mainTexture = screenCap;
	}


	// Use this for initialization
	void Start () {

		setBorder ();

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {

			StartCoroutine ("capture");

		}

	}


}
