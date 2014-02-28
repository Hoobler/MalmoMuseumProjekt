using UnityEngine;
using System.Collections;

public class QuestBase : MonoBehaviour {

	public virtual void TriggerStart(){


	}

	public virtual void TriggerFinish(){

		Instantiate (Resources.Load ("QuestEndDialogue"));
		}
}
