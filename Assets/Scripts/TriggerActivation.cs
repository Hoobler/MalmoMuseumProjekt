using UnityEngine;
using System.Collections;

public class TriggerActivation : QuestBase {

	public int collected = 0;
	int nrOfBags = 6;
	
	bool carrying = false;
	public bool questAccpeted = false;
	
	public Texture collectedTexture;

	ArrayList leaveArray = new ArrayList();
	ArrayList pickupArray = new ArrayList();

	GameObject quest1gui;


	void Start(){

	}
	
	void OnGUI(){
		if (questAccpeted) 
			GUI.Label (new Rect (70, 30, 30, 20), collected.ToString () + " / 6");
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Pickup" && !carrying && questAccpeted) {	
			other.gameObject.SetActive(false);
			carrying = true;
		}

		if (other.gameObject.tag == "Leavearea" && carrying == true) {

			carrying = false;
			GameObject go = (GameObject)leaveArray[collected];
			collected++;
			go.renderer.enabled = true;
			if(collected >= nrOfBags)
				TriggerFinish ();
		}

	}

	public override void TriggerStart ()
	{
		//start()
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Leavebag"))
			leaveArray.Add(go);
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pickup")) 
			pickupArray.Add(go);		
		
		if (!questAccpeted) {
			for(int i = 0; i < pickupArray.Count; i++){
				GameObject go = (GameObject)pickupArray[i];
				go.renderer.enabled = false;
			}
		}
		
		quest1gui = GameObject.FindWithTag("Quest1");
		collected = 0;
		//showpickupquest()
		questAccpeted = true;
		for(int i = 0; i < pickupArray.Count; i++) {
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = true;
		}
		quest1gui.SetActive (true);
	}

	public override void TriggerFinish()
	{
		for(int i = 0; i < pickupArray.Count; i++){
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = false;
		}

		questAccpeted = false;

		Debug.Log ("QUEST FINISHED");
	}
}


