using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid  : MonoBehaviour
	, IPointerClickHandler 
	, IDragHandler
	, IPointerEnterHandler
	, IPointerExitHandler
	, IPointerDownHandler
	, IPointerUpHandler
	, ISelectHandler
	{

	private Texture2D screenCap;


	public bool addBackground = false;
	private Texture2D backgroundTexture;


	[Range(4,32)]public int size = 5;

	[Range(0, 10)] public int xOffset = 5;  
	[Range(0, 10)] public int yOffset = 5;  

	[Range(200, 1267)] public int xPosition = 550;  
	[Range(0, 663)] public int yPosition = 50;  
	[Range(10f, 50f)] public float length = 50f;   


	public Button clearButton = null;
	public Button captureButton = null;
	public Button colorButton = null;

	public Image imagePrefab;
	private List <Image> images = new List<Image> ();

	private GameObject clickedImage;
	private GameObject onEnterImage;
	private GameObject onExitImage;

	public Color onColor = Color.green;
	public Color offColor = Color.black;

	private int width = 300;
	private int height = 663;

	private bool takeShot = false;


	void Awake () {

		if (clearButton != null && captureButton != null && colorButton != null) {
			clearButton.GetComponent<Button> ().onClick.AddListener (() => {
				OnClearEvent ();
			}); 

			captureButton.GetComponent<Button> ().onClick.AddListener (() => {
				OnCaptureEvent ();
			}); 

			colorButton.GetComponent<Button> ().onClick.AddListener (() => {
				OnRandomColorEvent ();
			}); 

		}else{
			Debug.Log ("BUTTONS ARE NOT LINKED");
		}

		if (addBackground){
			setBackgroundImage ();
		}

		setGrid ();

	}

	void OnClearEvent()
	{
		clearGridColors ();

	}

	void OnCaptureEvent()
	{
		StartCoroutine ("capture");
	}

	void OnRandomColorEvent (){

		onColor = ExtensionMethods.RandomColor ();
	}



	void Start() {

		width = ((int)(length) + xOffset) * size;
		height = Screen.height;

		screenCap = new Texture2D( width, height, TextureFormat.RGB24, false);  

	}

	void Update(){

		clearButton.GetComponent<Image>().color = Color.white;
		captureButton.GetComponent<Image>().color = Color.white;
		colorButton.GetComponent<Image>().color = Color.white;

//		if (Input.GetKeyDown(KeyCode.Space)){
//
//			clearGridColors ();
//		}
//
//
//		if (Input.GetKeyDown (KeyCode.S)) {
//
//			StartCoroutine ("capture");
//
//		}
//		if (Input.GetKeyDown (KeyCode.C)) {
//
//			onColor = ExtensionMethods.RandomColor ();
//
//		}


	}



	void setBackgroundImage(){

		width = ((int)(length) + xOffset) * size;
		height = Screen.height;


		backgroundTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
		backgroundTexture.name = "background";

		backgroundTexture.Apply ();

	}
	void OnGUI(){

		if(addBackground){
			
			GUI.DrawTexture (new Rect (new Vector2 (xPosition - (xOffset/2) , 0), new Vector2 ( width, height)), backgroundTexture, ScaleMode.StretchToFill); //top
		}

		if (takeShot) {

			GUI.DrawTexture(new Rect( 10, 10, 100, 100), screenCap, ScaleMode.StretchToFill); //result
		}


	}


	IEnumerator capture(){

		yield return new WaitForEndOfFrame ();

		Rect captureFrame = new Rect (xPosition - (xOffset/2) , 0, width, height);

		screenCap.ReadPixels(captureFrame,0,0,true);
		screenCap.Apply();

		takeShot = true;

	}


	void setGrid(){

		if (imagePrefab != null) {

			for (int i = 0, y = 0; y <= size - 1; y++) {

				for (int x = 0; x <= size - 1; x++, i++) {

					Image img = (Image)Instantiate (imagePrefab);

					img.transform.position = new Vector3 ((x * (length + xOffset)) + xPosition, (y * -(length + yOffset)) - yPosition);
					img.transform.SetParent (transform, false);  
					img.rectTransform.sizeDelta = new Vector2 (length, length);
					img.color = offColor;
					img.name = "block" + i;

					images.Add (img);
				}
			}
		} else {

			Debug.Log ("IMAGE PREFAB IS NOT LINKED");
		}


	}

	void clearGridColors(){

		if (images.Count > 0) {

			foreach (Image img in images) {

				img.color = offColor;
			}
		}

	}


	public void OnPointerClick(PointerEventData eventData) // 3
	{
		
//		clickedImage = eventData.pointerPressRaycast.gameObject;
//
//		if (clickedImage.name != "background" || clickedImage.name != "ClearButton" || clickedImage.name != "CaptureButton") {
//			
//			print (eventData.pointerPressRaycast.gameObject.name + "  children: " + clickedImage.transform.childCount);
//
//			Color rand = ExtensionMethods.RandomColor ();
//			clickedImage.GetComponent<Image> ().color = rand;
//		}

	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Debug.Log ("OnPointerEnter "+ eventData.pointerEnter.gameObject.name);


		onEnterImage = eventData.pointerEnter.gameObject;

		if (onEnterImage.name != "ClearButton" || onEnterImage.name != "CaptureButton") {
			onEnterImage.GetComponent<Image> ().color = onColor;
		} else {

			onEnterImage.GetComponent<Image> ().color = Color.white;
		}


	}
	public void OnPointerExit (PointerEventData eventData)
	{
		Debug.Log ("OnPointerExit"+ eventData.pointerEnter.gameObject.name);

//		onExitImage = eventData.pointerEnter.gameObject;
//		onExitImage.GetComponent<Image> ().color = onColor;


	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log ("OnPointerDown");
	}
	public void OnPointerUp (PointerEventData eventData)
	{
		Debug.Log ("OnPointerUp");
	}
	public void OnSelect (BaseEventData eventData)
	{
		Debug.Log ("OnSelect");
	}
	public void OnDrag(PointerEventData eventData)
	{
		print("I'm being dragged!");
	}



}




