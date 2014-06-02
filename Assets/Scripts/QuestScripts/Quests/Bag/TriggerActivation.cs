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

	GameObject endDiag;
	GameObject quest1gui;
	GameObject holdBag;
	GameObject guiTexture;
	GUITexture temp;
	GameObject reminder;
	GUIText collectedText;
	GUIText timeText;

	void Start(){
		if (Application.loadedLevelName == "LillaTorg") {
			reminder = (GameObject)Instantiate (Resources.Load ("ReminderText"));
			reminder.transform.parent = GameObject.Find ("BagQuest").transform;
			endDiag = (GameObject)Instantiate(Resources.Load("QuestEndDialogue"));
			endDiag.transform.parent = GameObject.Find ("BagQuest").transform;
		}
	}

	void Update(){
		if (questAccpeted) {
			timeElapsed += Time.deltaTime;
			timeElapsed = Mathf.Round(timeElapsed * 100.0f) / 100.0f;
			collectedText.text = collected.ToString() + " / 6";
			timeText.text = timeElapsed.ToString();
		}
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
			(other.gameObject.GetComponent("Halo") as Behaviour).enabled = false;
			other.gameObject.renderer.enabled = false;
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
				TriggerFinish (true);
		}

	}

	public override void TriggerStart ()
	{
		holdBag = GameObject.FindGameObjectWithTag("HoldBag");
		guiTexture = GameObject.Find ("GUIBags");
		temp = guiTexture.GetComponent ("GUITexture") as GUITexture;
		temp.enabled = true;

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Leavebag"))
			leaveArray.Add(go);
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pickup")) 
			pickupArray.Add(go);		
		
		quest1gui = GameObject.FindWithTag("Quest1");
		collected = 0;

		questAccpeted = true;
		for(int i = 0; i < pickupArray.Count; i++) {
			GameObject go = (GameObject)pickupArray[i];
			go.SetActive(true);
			go.renderer.enabled = true;
			(go.GetComponent("Halo") as Behaviour).enabled = true;
		}
		quest1gui.SetActive (true);
		temp.enabled = true;

		InitText ();

		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript>()).ChangeText("Hjälp mig hitta mina vetepåsar och lämna tillbaka dom till mig. Du plockar upp dom när du går in i dom. DU lämnar in dom genom att gå in i området bredvid mig.");
		AndroidDisableArgs args = new AndroidDisableArgs();
		args.Left = true;
		args.Right = true;
		EventManager.TriggerDisableAndroid(args);
	}

	public override void TriggerFinish(bool success)
	{
		base.TriggerFinish (success);

		//PREFS
		if (PlayerPrefs.GetInt ("LTquest") == 0)
			PlayerPrefs.SetInt ("LTquest", 1);
		else if (PlayerPrefs.GetInt ("LTquest") == 2)
			PlayerPrefs.SetInt ("LTquest", 3);
		//---

		for(int i = 0; i < pickupArray.Count; i++){
			GameObject go = (GameObject)pickupArray[i];
			go.SetActive(false);
		}

		questFinished = true;
		questAccpeted = false;
		temp.enabled = false;

		reminder.SetActive (false);
		Destroy (GameObject.Find ("CollectedText"));
		Destroy (GameObject.Find ("TimeText"));

		endDiag.GetComponent<endNotificationScript> ().Activate("Tack du hämtade alla påsar");
		endDiag.SetActive (true);

		Reset ();
		
	}

	private void InitText(){
		GameObject collected = new GameObject ("CollectedText");
		collectedText = (GUIText)collected.AddComponent<GUIText> ();
		collectedText.pixelOffset = new Vector2 (Screen.width * 0.05f, Screen.height * 0.96f);
		collectedText.fontSize = (int)(12 * Screen.width / 800f);
		collectedText.anchor = TextAnchor.MiddleLeft;
		collectedText.alignment = TextAlignment.Left;
		collectedText.text = collected.ToString() + " / 6";

		GameObject time = new GameObject ("TimeText");
		timeText = (GUIText)time.AddComponent<GUIText> ();
		timeText.pixelOffset = new Vector2 (Screen.width * 0.05f, Screen.height * 0.92f);
		timeText.fontSize = (int)(12 * Screen.width / 800f);
		timeText.anchor = TextAnchor.MiddleLeft;
		timeText.alignment = TextAlignment.Left;
		timeText.text = timeElapsed.ToString();

	}
}


