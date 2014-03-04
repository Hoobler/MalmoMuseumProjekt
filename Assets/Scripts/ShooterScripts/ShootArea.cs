using UnityEngine;
using System.Collections;

public class ShootArea : MonoBehaviour {

	public bool DisableMeshRenderer;

	void Start(){
		if(DisableMeshRenderer){
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			EventManager.TriggerOnQuest(QuestTypeEnum.Trigger, "EnterShootArea");
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			EventManager.TriggerOnQuest(QuestTypeEnum.Trigger, "ExitShootArea");
		}
	}
}
