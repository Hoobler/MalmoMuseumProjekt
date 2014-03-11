using UnityEngine;
using System.Collections;

public class MusketQuest : MonoBehaviour {

	public int HitsToFinish;

	private bool _questStarted;
	private bool _questFinished;
	private int _hits;

	// Use this for initialization
	void Start () {
		EventManager.OnQuest += EventRespons;

		_questFinished = false;
		_questStarted = false;
		_hits = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(_hits >= HitsToFinish){
			_questFinished = true;
		}
		if(_questFinished){
			QuestFinished();
		}
	}

	void EventRespons(MiniGamesEnum miniEnum, QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.OnGoing){
				if(evArgs.Info == "Hit" && _questStarted){
					_hits++;
				}
			}
			if(evArgs.QuestType == QuestTypeEnum.Started){
				_questStarted = true;
			}
		}
	}

	void QuestFinished(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Finnished, null));
	}

	void ResetQuest(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Reset, null));
		_hits = 0;
	}
}
