using UnityEngine;
using System.Collections;

public class MusketQuest : MonoBehaviour {

	public int HitsToFinish;
	public float TimeLimit;

	private bool _questStarted;
	private bool _questFinished;
	private bool _firstHit;
	private int _hits;
	private float _timeElapsed;
	
	void Start () {
		EventManager.OnQuest += EventRespons;

		_questFinished = false;
		_questStarted = false;
		_firstHit = false;
		_hits = 0;
	}

	void Update () {
		if(_hits >= HitsToFinish && _timeElapsed <= TimeLimit){
			_questFinished = true;
		}
		if(_questFinished){
			QuestFinished();
		}
		if(_questStarted && !_questFinished){
			_timeElapsed += Time.deltaTime;
		}
	}

	void OnGUI(){
		if (_questStarted) {
			//GUI.Label (new Rect (70, 30, 30, 20), collected.ToString () + " / 6");
			GUI.Label (new Rect (70, 50, 30, 20), _timeElapsed.ToString());
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

	// Add more to this at a later date!
	void QuestFinished(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Finnished, null));
	}

	void ResetQuest(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Reset, null));
		_hits = 0;
		_timeElapsed = 0;
	}
}
