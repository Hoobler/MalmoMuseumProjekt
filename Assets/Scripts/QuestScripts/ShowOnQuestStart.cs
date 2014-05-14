using UnityEngine;
using System.Collections;

public class ShowOnQuestStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
//		EventManager.OnQuest += EventRespons;
	}
	
	void EventRespons(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		Debug.Log("ShowOnQuest");
//		if(miniEnum == MiniGamesEnum.Musköt){
//			Debug.Log("miniEnum: " + miniEnum);
//			Debug.Log("ShowOnQuest inside miniEnum");
//			if(evArgs.QuestType == QuestTypeEnum.Started){
//				Debug.Log("ShowOnQuest evArgs QuestStarted");
//				if(this.renderer != null){
//					Debug.Log("Showonquest not null");
//				}
//				if(this.renderer == null){
//					Debug.Log("Showonquest is null");
//				}
//				this.renderer.enabled = true;
//			}
//			if(evArgs.QuestType == QuestTypeEnum.Finnished){
//				Debug.Log("ShowOnQuest evArgs QuestFin");
//				this.renderer.enabled = false;
//			}
//		}
	}
}
