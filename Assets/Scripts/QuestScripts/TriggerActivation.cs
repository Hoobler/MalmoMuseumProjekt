using UnityEngine;
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
	GameObject guiTexture;
	GUITexture temp;
	GameObject reminder;

	void Start(){
		holdBag = GameObject.FindGameObjectWithTag("HoldBag");
		guiTexture = GameObject.Find ("GUIBags");
		temp = guiTexture.GetComponent ("GUITexture") as GUITexture;
		temp.enabled = false;
		reminder = (GameObject)Instantiate (Resources.Load ("ReminderText"));
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

	void Reset(){

		for (int i = 0; i < leaveArray.Count; i++) {
			GameObject go = (GameObject)leaveArray [i];
			go.renderer.enabled = false;
		}

		collected = 0;
		questFinished = false;
		timeElapsed = 0;
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Pickup" && !carrying && questAccpeted) {	
			other.gameObject.renderer.enabled = false;
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
				TriggerFinish (true);
		}

	}

	public override void TriggerStart ()
	{
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Leavebag"))
			leaveArray.Add(go);
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pickup")) 
			pickupArray.Add(go);		
		
		quest1gui = GameObject.FindWithTag("Quest1");
		collected = 0;

		questAccpeted = true;
		for(int i = 0; i < pickupArray.Count; i++) {
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = true;
			(go.GetComponent("Halo") as Behaviour).enabled = true;
		}
		quest1gui.SetActive (true);
		temp.enabled = true;

		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript>()).ChangeText("Hjälp mig hitta mina vetepåsar och lämna tillbaka dom till mig. Du plockar upp dom när du går in i dom. DU lämnar in dom genom att gå in i området bredvid mig.");
	}

	public override void TriggerFinish(bool success)
	{
		base.TriggerFinish (success);

		for(int i = 0; i < pickupArray.Count; i++){
			GameObject go = (GameObject)pickupArray[i];
			go.renderer.enabled = false;
		}
		questFinished = true;
		questAccpeted = false;
		temp.enabled = false;

		GameObject endDiag = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		GUIText endText = (GUIText)endDiag.GetComponentInChildren (typeof(GUIText));
		endText.text = "Tack, du hämtade alla påsar!";
		reminder.SetActive (false);
		Reset ();
		
	}
}


