using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public void ActivateQuest(string name)
	{
		QuestBase tact = GameObject.FindGameObjectWithTag ("Player").GetComponent ("TriggerActivation") as QuestBase;
		if (name == "LillaTorgBagQuest") {
			tact.TriggerStart();
		}
	}

}
