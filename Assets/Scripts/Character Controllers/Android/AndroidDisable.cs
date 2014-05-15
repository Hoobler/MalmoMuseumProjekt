using UnityEngine;
using System.Collections;

public class AndroidDisable: MonoBehaviour {

	public GameObject RightPad;
	public GameObject LeftPad;

	// Use this for initialization
	void Start () {
		EventManager.DisableAndroidEvent += new DisableAndroid(DisableThumbPads);
		LeftPad = GameObject.Find("LeftTouchPad");
		RightPad = GameObject.Find("RightTouchPad");
	}

	void DisableThumbPads(object o ,AndroidDisableArgs e){
		Debug.Log("Disable test");
		if(e.Disable){
			LeftPad.SetActive(false);
			RightPad.SetActive(false);
		}
		if(!e.Disable){
			LeftPad.SetActive(true);
			RightPad.SetActive(true);
		}
	}
}
