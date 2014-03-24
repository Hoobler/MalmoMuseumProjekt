using UnityEngine;
using System.Collections;

public class AndroidDisable: MonoBehaviour {

	public GameObject RightPad;
	public GameObject LeftPad;

	// Use this for initialization
	void Start () {
		EventManager.OnDisable += EventRespons;
	}

	void EventRespons(string type){
		//Debug.Log("Disable test");
		if(type == "lock"){
			LeftPad.SetActive(false);
			RightPad.SetActive(false);
		}
		if(type == "unlock"){
			LeftPad.SetActive(true);
			RightPad.SetActive(true);
		}
	}
}
