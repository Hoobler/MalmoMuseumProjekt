using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateQuest(string name)
	{
		QuestBase tact = GameObject.FindGameObjectWithTag ("Player").GetComponent ("TriggerActivation") as QuestBase;

		if (name == "LillaTorgBagQuest") {
			tact.Trigger();
		}
	}

}
