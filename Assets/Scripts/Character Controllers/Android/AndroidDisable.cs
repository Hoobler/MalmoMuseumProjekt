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
		if(e.Disable){
//			LeftPad.SetActive(false);
//			RightPad.SetActive(false);
			LeftPad.GetComponent(GUITexture).renderer.enabled = false;
			RightPad.GetComponent(GUITexture).renderer.enabled = false;
			Debug.Log("Disable activeSelf: " + TouchPads[0].activeSelf);
		}
		if(!e.Disable){
//			LeftPad.SetActive(true);
//			RightPad.SetActive(true);
			LeftPad.renderer.enabled = true;
			RightPad.renderer.enabled = true;
			Debug.Log("Enable activeSelf: " + TouchPads[0].activeSelf);
		}
	}
}
