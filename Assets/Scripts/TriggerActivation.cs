using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour {

	public int collected = 0;

	bool carrying = false;
	bool shown = false;
	
	public Texture collectedTexture;

	void Update(){
		if (collected == 6 && shown == false) {
			shown = true;
		}
	}

	void OnGUI(){
		GUI.Label (new Rect (70, 30, 30, 20), collected.ToString() + " / 6");
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Pickup" && carrying == false) {	
			other.gameObject.SetActive(false);
			carrying = true;
		}

		if (other.gameObject.tag == "Leavearea" && carrying == true) {
			collected++;
			carrying = false;
		}
	}
}
