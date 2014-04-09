using UnityEngine;
using System.Collections;

public class ShowOnQuestStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
		EventManager.OnQuest += EventRespons;
	}
	
	void EventRespons(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.Started){
				this.renderer.enabled = true;
			}
			if(evArgs.QuestType == QuestTypeEnum.Finnished){
				this.renderer.enabled = false;
			}
		}
	}
}
