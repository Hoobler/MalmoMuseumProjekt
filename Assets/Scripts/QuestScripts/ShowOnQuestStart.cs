using UnityEngine;
using System.Collections;

public class ShowOnQuestStart : MonoBehaviour {

	private GameObject musket;

	// Use this for initialization
	void Start () {
		musket = GameObject.FindGameObjectWithTag("Musket");
		EventManager.QuestEvent += new QuestHandler(ShowOnQuest);
	}
	
	void ShowOnQuest(object o, QuestEventArgs e){
		Debug.Log("ShowOnQuest");
		if(e.MiniGames == MiniGamesEnum.Musköt){
			Debug.Log("miniEnum: " + e.MiniGames);
			Debug.Log("ShowOnQuest inside miniEnum");
			if(e.QuestType == QuestTypeEnum.Started){

				Debug.Log("ShowOnQuest evArgs QuestStarted");
				if(musket != null)
					musket.renderer.enabled = true;
		
			}
			if(e.QuestType == QuestTypeEnum.Finnished){
				Debug.Log("ShowOnQuest evArgs QuestFin");
				if(musket != null)
					musket.renderer.enabled = false;
			}
		}
	}
}
