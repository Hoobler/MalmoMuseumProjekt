using UnityEngine;
using System.Collections;

public class TriggerActivation : QuestBase {

	public int collected = 0;
	
	bool carrying = false;
	public bool questAccpeted = false;
	
	public Texture collectedTexture;

	ArrayList leaveArray = new ArrayList();
	ArrayList pickupArray = new ArrayList();

	GameObject quest1gui;


	void Start(){
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
	//	quest1gui.SetActive (false);
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
		}

		if (other.gameObject.tag == "Questgiver" && questAccpeted == false) {
			ShowPickupQuest();
		}
	}

	public void ShowPickupQuest(){
		questAccpeted = true;
		for(int i = 0; i < pickupArray.Count; i++) {
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = true;
		}
		quest1gui.SetActive (true);

	}

	public override void Trigger ()
	{
		questAccpeted = true;
		for(int i = 0; i < pickupArray.Count; i++) {
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = true;
		}
		quest1gui.SetActive (true);
	}
}


