using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour {

	public bool test = false;

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (test) {
			GUI.Label(new Rect(10, 10, 100, 100), "yey");		
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Pickup") {	
			other.gameObject.SetActive(false);
			test = true;

		}
	}

}
