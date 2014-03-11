﻿using UnityEngine;
using System.Collections;

public class TriggerActivation : QuestBase {

	public int collected = 0;
	int nrOfBags = 6;
	
	bool carrying = false;
	public bool questAccpeted = false;
	public bool questFinished = false;
	private float timeElapsed = 0.0f;
	
	public Texture collectedTexture;

	ArrayList leaveArray = new ArrayList();
	ArrayList pickupArray = new ArrayList();

	GameObject quest1gui;
	GameObject holdBag;

	void Start(){
		holdBag = GameObject.FindGameObjectWithTag("HoldBag");
	}
	
	void OnGUI(){
		if (questAccpeted) {
			GUI.Label (new Rect (70, 30, 30, 20), collected.ToString () + " / 6");
			GUI.Label (new Rect (70, 50, 30, 20), timeElapsed.ToString());
		}
			
	}

	void Update(){
		if (questAccpeted)
			timeElapsed += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Pickup" && !carrying && questAccpeted) {	
			other.gameObject.SetActive(false);
			carrying = true;
			holdBag.renderer.enabled = true;
		}

		if (other.gameObject.tag == "Leavearea" && carrying == true) {

			holdBag.renderer.enabled = false;
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
		questFinished = true;
		questAccpeted = false;

		Debug.Log ("QUEST FINISHED");
		base.TriggerFinish ();
	}
}

