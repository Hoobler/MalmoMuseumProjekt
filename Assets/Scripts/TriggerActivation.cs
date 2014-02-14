using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour {

	public int collected = 0;
	
	bool carrying = false;
	
	public Texture collectedTexture;

	ArrayList array = new ArrayList();
	

	void Start(){
		foreach(GameObject gameObj in GameObject.FindGameObjectsWithTag("Leavebag")){
			array.Add(gameObj);
		}
	}

	void Update(){

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

			carrying = false;
			GameObject go = (GameObject)array[collected];
			collected++;
			go.renderer.enabled = true;
		}
	}
}
