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
		if(type == "lock"){
			RightPad.SetActive(false);
			RightPad.SetActive(false);
		}
		if(type == "unlock"){
			RightPad.SetActive(true);
			RightPad.SetActive(true);
		}
	}
}
