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
			Debug.Log("SetActive False");
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
