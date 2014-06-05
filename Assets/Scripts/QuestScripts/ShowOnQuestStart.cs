using UnityEngine;
using System.Collections;

public class ShowOnQuestStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
		EventManager.QuestEvent += new QuestHandler(ShowOnQuest);
	}
	
	void ShowOnQuest(object o, QuestEventArgs e){
		Debug.Log("ShowOnQuest");
		if(e.MiniGames == MiniGamesEnum.Musköt){
			Debug.Log("miniEnum: " + e.MiniGames);
			Debug.Log("ShowOnQuest inside miniEnum");
			if(e.QuestType == QuestTypeEnum.Started){

				Debug.Log("ShowOnQuest evArgs QuestStarted");
//				if(gameObject.renderer != null){
//					Debug.Log("Showonquest not null");
//				}
//				else{
//					Debug.Log("Showonquest is null");
//				}
				if(gameObject != null)
					gameObject.renderer.enabled = true;
			}
			if(e.QuestType == QuestTypeEnum.Finnished){
				Debug.Log("ShowOnQuest evArgs QuestFin");
			if(gameObject.renderer != null)
				gameObject.renderer.enabled = false;
			}
		}
	}
}
