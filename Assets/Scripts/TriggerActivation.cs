using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		this.gameObject.SetActive (false);
	}
}
