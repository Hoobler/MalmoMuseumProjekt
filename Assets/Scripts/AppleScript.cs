﻿using UnityEngine;
using System.Collections;

public class AppleScript : MonoBehaviour {

	ThrowQuest throwRef;
	float timeUntilRemoval;
	bool isCountingDown;
	bool hasTriggered;

	void Start()
	{
		throwRef = GameObject.Find ("AppleQuestPerson").GetComponent (typeof(ThrowQuest)) as ThrowQuest;
		timeUntilRemoval = 5.0f;
		hasTriggered = false;
	}

	void Update()
	{
			timeUntilRemoval -= Time.deltaTime;
			if (timeUntilRemoval <= 0) {
				throwRef.AppleTrigger (false);
				throwRef.FinishCheck();
				Destroy (gameObject);
			}

	}

	void OnCollisionEnter(Collision collision)
	{
		if (timeUntilRemoval >= 3)
			timeUntilRemoval = 3f;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (!hasTriggered) {

			if (timeUntilRemoval >= 1)
				timeUntilRemoval = 1f;
			if (collider.gameObject.name == "QuestBasketTrigger") {
				hasTriggered = true;
				if (throwRef != null) {
					throwRef.AppleTrigger (true);
				}
			}
		}
	}

}
