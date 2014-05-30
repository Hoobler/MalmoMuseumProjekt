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
		TouchPads = new GameObject[2];
		TouchPads = GameObject.FindGameObjectsWithTag("TouchPad");
	}

	void OnEnable(){
		LeftPad = GameObject.Find("LeftTouchPad");
		RightPad = GameObject.Find("RightTouchPad");
		Debug.Log("Name: " + LeftPad.name + "Herp: " + LeftPad);
	}

	void DisableThumbPads(object o ,AndroidDisableArgs e){
		Debug.Log("Disable test");
		Debug.Log("Args: " + e.Disable);
//		if(TouchPads[0]){
//			Debug.Log("is not null activeSelf: " + TouchPads[0].activeSelf);
//		}else{
//			Debug.Log("Leftpad is null");
//		}
		Debug.Log("LeftPad activeSelf: " + LeftPad.activeSelf);
		Debug.Log("RightPad activeSelf: " + RightPad.activeSelf);
		if(e.Disable){
			Debug.Log("Disable derp");
//			LeftPad.SetActive(false);
//			RightPad.SetActive(false);
//			LeftPad.guiTexture.renderer.enabled = false;
//			RightPad.guiTexture.renderer.enabled = false;
			Debug.Log("After disable");
			Debug.Log("Disable activeSelf: " + TouchPads[0].activeSelf);
		}
		if(!e.Disable){
//			LeftPad.SetActive(true);
//			RightPad.SetActive(true);
			LeftPad.guiTexture.renderer.enabled = true;
			RightPad.guiTexture.renderer.enabled = true;
			Debug.Log("Enable activeSelf: " + TouchPads[0].activeSelf);
		}
	}
}
