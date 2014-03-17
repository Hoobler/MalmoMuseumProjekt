using UnityEngine;
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
				throwRef.FinishCheck();
				Destroy (gameObject);
			}

	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("APPLECOLLISION");
		if (throwRef != null)
			throwRef.AppleTrigger (false);
		if (timeUntilRemoval >= 3)
			timeUntilRemoval = 3f;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (!hasTriggered) {
			hasTriggered = true;
			if (timeUntilRemoval >= 1)
				timeUntilRemoval = 1f;
			Debug.Log ("APPLETRIGGER");
			if (collider.gameObject.name == "QuestBasketTrigger") {
				if (throwRef != null) {
					throwRef.AppleTrigger (true);
				}
			}
		}
	}

}
