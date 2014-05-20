using UnityEngine;
using System.Collections;

public class AndroidDisable: MonoBehaviour {

	private GameObject RightPad;
	private GameObject LeftPad;
	private GameObject[] TouchPads;

	// Use this for initialization
	void Start () {
		EventManager.DisableAndroidEvent += new DisableAndroid(DisableThumbPads);
//		LeftPad = GameObject.Find("LeftTouchPad");
//		RightPad = GameObject.Find("RightTouchPad");
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
		Debug.Log("Args: " + e.Disable);
		if(TouchPads[0]){
			Debug.Log("is not null");
		}else{
			Debug.Log("Leftpad is null");
		}
		if(e.Disable){
			LeftPad.SetActive(false);
			RightPad.SetActive(false);
		}
		if(!e.Disable){
			Debug.Log("SetActive True");
			LeftPad.SetActive(true);
			RightPad.SetActive(true);
		}
	}
}
