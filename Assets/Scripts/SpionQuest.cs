using UnityEngine;
using System.Collections;

public class SpionQuest : QuestBase {


	GameObject spionA;
	GameObject spionB;
	GameObject spionC;

	bool questActive;


	// Use this for initialization
	void Start () {
		spionA = GameObject.Find ("SpionA");
		spionB = GameObject.Find ("SpionB");
		spionC = GameObject.Find ("SpionC");
		questActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void TriggerStart ()
	{
		questActive = false;

	}

	public override void TriggerFinish ()
	{


	}

	void SpyCheck(int chosen)
	{

	}

	public void SpyTrigger(GameObject other)
	{
		if (other.name == "SpionA")
			SpyCheck(0);
		else if (other.name == "SpionB")
			SpyCheck(1);
		else if(other.name == "SpionC")
			SpyCheck(2);
	}
}
