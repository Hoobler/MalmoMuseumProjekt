using UnityEngine;
using System.Collections;

public class ShootArea : MonoBehaviour {

	public bool DisableMeshRenderer;

	private bool _questStared;

	void Start(){
		if(DisableMeshRenderer){
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger Enter" + other.name);
		if(other.tag == "Player"){
			Debug.Log("Trigger Player");
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt ,new QuestEventArgs(QuestTypeEnum.Trigger, "EnterShootArea"));
			EventManager.TriggerDisableAndroid("lock");
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt ,new QuestEventArgs(QuestTypeEnum.Trigger, "ExitShootArea"));
		}
	}	
}
