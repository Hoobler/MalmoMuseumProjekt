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
		Debug.Log("Name: " + LeftPad.name + "Herp: " + LeftPad);
//		TouchPads = new GameObject[2];
//		TouchPads = GameObject.FindGameObjectsWithTag("TouchPad");
	}

	void OnEnable(){
		LeftPad = GameObject.Find("LeftTouchPad");
		RightPad = GameObject.Find("RightTouchPad");
		Debug.Log("Name: " + LeftPad.name + "Herp: " + LeftPad);
	}

	void DisableThumbPads(object o ,AndroidDisableArgs e){
		Debug.Log("Disable test");
		Debug.Log("Args: " + e.Left + e.Right);
//		if(TouchPads[0]){
//			Debug.Log("is not null activeSelf: " + TouchPads[0].activeSelf);
//		}else{
//			Debug.Log("Leftpad is null");
//		}
//		Debug.Log("LeftPad activeSelf: " + LeftPad.activeSelf);
//		Debug.Log("RightPad activeSelf: " + RightPad.activeSelf);

			Debug.Log("Disable derp");
			LeftPad.SetActive(e.Left);
			RightPad.SetActive(e.Right);
//			LeftPad.guiTexture.enabled = false;
//			RightPad.guiTexture.enabled = false;
//			Debug.Log("After disable");
//			Debug.Log("Disable activeSelf: " + TouchPads[0].activeSelf);


	}
}
