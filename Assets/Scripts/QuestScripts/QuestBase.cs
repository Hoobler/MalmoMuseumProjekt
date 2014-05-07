using UnityEngine;
using System.Collections;

public class QuestBase : MonoBehaviour {

	public virtual void TriggerStart(){


	}

	public virtual void TriggerFinish(bool success){
		GameObject.Find ("Quest_Handler").GetComponent<QuestManager> ().QuestFinished ();
	}
}
