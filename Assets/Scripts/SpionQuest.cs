using UnityEngine;
using System.Collections;

public class SpionQuest : QuestBase {

	bool questActive;


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
		Debug.Log("SPION START");
	}

	public override void TriggerFinish ()
	{


	}
}
