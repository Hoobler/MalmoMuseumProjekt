using UnityEngine;
using System.Collections;

public class QuestMarker : MonoBehaviour {

	public Texture2D startQuest;
	public Texture2D acceptedQuest;
	public Texture2D finishedQuest;
	
	TriggerActivation tact;

	// Update is called once per frame
	void Update () {
		Vector3 rot =  Camera.main.transform.rotation.eulerAngles;
		rot.x = 90;
		rot.z = 0;
		rot.y += 180;
		transform.rotation = Quaternion.Euler(rot);

		tact = GameObject.FindGameObjectWithTag ("Player").GetComponent ("TriggerActivation") as TriggerActivation;

		if (!tact.questAccpeted && !tact.questFinished)
						this.renderer.material.mainTexture = startQuest;
		else if (tact.questAccpeted && !tact.questFinished)
						this.renderer.material.mainTexture = acceptedQuest;
		else if (!tact.questAccpeted && tact.questFinished)
						this.renderer.material.mainTexture = finishedQuest;
	}
}
