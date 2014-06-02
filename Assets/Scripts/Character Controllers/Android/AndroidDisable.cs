using UnityEngine;
using System.Collections;

public class AndroidDisable: MonoBehaviour {

	private GameObject RightPad;
	private GameObject LeftPad;
	private GameObject[] TouchPads;

	// Use this for initialization
	void Start () {
		EventManager.DisableAndroidEvent += new DisableAndroid(DisableThumbPads);
		LeftPad = GameObject.Find("LeftTouchPad");
		RightPad = GameObject.Find("RightTouchPad");
		if(LeftPad == null)
		{
			Debug.Log ("Left pad is null at start");
		}
		if(RightPad == null)
		{
			Debug.Log ("Right pad is null at start");
		}
		Debug.Log ("I EXIST");
//		TouchPads = new GameObject[2];
//		TouchPads = GameObject.FindGameObjectsWithTag("TouchPad");
	}

	void DisableThumbPads(object o ,AndroidDisableArgs e){

//		if(TouchPads[0]){
//			Debug.Log("is not null activeSelf: " + TouchPads[0].activeSelf);
//		}else{
//			Debug.Log("Leftpad is null");
//		}
//		Debug.Log("LeftPad activeSelf: " + LeftPad.activeSelf);
//		Debug.Log("RightPad activeSelf: " + RightPad.activeSelf);

		if(LeftPad == null)
		{
			LeftPad = GameObject.Find("LeftTouchPad");
			Debug.Log ("Left pad was null");
			if(LeftPad == null)
				Debug.Log ("... and still is");
		}
		if(RightPad == null)
		{
			RightPad = GameObject.Find("RightTouchPad");
			Debug.Log ("Right pad was null");
			if(RightPad == null)
				Debug.Log ("... and still is");
		}
		if(LeftPad != null)
			LeftPad.SetActive(e.Left);
		if(RightPad != null)
			RightPad.SetActive(e.Right);
//			LeftPad.guiTexture.enabled = false;
//			RightPad.guiTexture.enabled = false;
//			Debug.Log("After disable");
//			Debug.Log("Disable activeSelf: " + TouchPads[0].activeSelf);


	}
}
