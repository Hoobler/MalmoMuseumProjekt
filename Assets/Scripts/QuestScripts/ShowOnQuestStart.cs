using UnityEngine;
using System.Collections;

public class ShowOnQuestStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
		EventManager.QuestEvent += new QuestHandler(ShowOnQuest);
	}
	
	void ShowOnQuest(object o, QuestEventArgs e){
//		Debug.Log("ShowOnQuest");
//		if(e.MiniGames == MiniGamesEnum.Musköt){
//			Debug.Log("miniEnum: " + e.MiniGames);
//			Debug.Log("ShowOnQuest inside miniEnum");
//			if(e.QuestType == QuestTypeEnum.Started){
//				Debug.Log("ShowOnQuest evArgs QuestStarted");
//				if(this.renderer != null){
//					Debug.Log("Showonquest not null");
//				}
//				if(this.renderer == null){
//					Debug.Log("Showonquest is null");
//				}
//				this.renderer.enabled = true;
//			}
//			if(e.QuestType == QuestTypeEnum.Finnished){
//				Debug.Log("ShowOnQuest evArgs QuestFin");
//				this.renderer.enabled = false;
//			}
//		}
	}
}
