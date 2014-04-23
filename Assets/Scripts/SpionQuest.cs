using UnityEngine;
using System.Collections;

public class SpionQuest : QuestBase {

	bool questActive;
	GameObject dialogueWindow;

	// Use this for initialization
	void Start () {
		questActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void TriggerStart ()
	{
		questActive = true;
		//dialogueWindow = (GameObject)Instantiate("
	}

	public override void TriggerFinish ()
	{


	}
}
