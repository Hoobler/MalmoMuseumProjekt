using UnityEngine;
using System.Collections;

[System.Serializable]
public class Points{
	public int BullsEye;
	public int SecondRing;
	public int ThirdRing;
}

public class MusketQuest : MonoBehaviour {
	
	public float TimeLimit;
	public Points Points;
	
	private bool _questStarted;
	private bool _questEnded;
	private bool _firstHit;
	private int _totalPoints;
	private float _timeElapsed;
	
	void Start () {
		EventManager.OnQuest += EventRespons;

		_questEnded = false;
		_questStarted = false;
		_firstHit = false;
		_totalPoints = 0;
	}

	void Update () {
		if(_firstHit && !_questEnded){
			if(_questStarted && !_questEnded){
				_timeElapsed += Time.deltaTime;
			}
			if(_timeElapsed >= TimeLimit){
				_questEnded = true;
				HighScore();
				QuestFinished();
			}
		}
	}

	void OnGUI(){
		if (_questStarted) {
			GUI.Label (new Rect (70, 30, 70, 20), "Poäng: " + _totalPoints.ToString());
			GUI.Label (new Rect (70, 50, 30, 20), _timeElapsed.ToString());
		}
	}

	void EventRespons(MiniGamesEnum miniEnum, QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.OnGoing){
				if(evArgs.Info == "BullsEye" && _questStarted){
					_totalPoints += Points.BullsEye;
				}
				if(evArgs.Info == "RedRing" && _questStarted){
					_totalPoints += Points.ThirdRing;
				}
				if(evArgs.Info == "WhiteRing" && _questStarted){
					_totalPoints += Points.SecondRing;
				}
			}
			if(evArgs.QuestType == QuestTypeEnum.Started){
				_questStarted = true;
			}
		}
	}

	void HighScore(){
		//stuffs at a later date!
	}	

	// Add more to this at a later date!
	void QuestFinished(){
		Debug.Log("Finnished");
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Finnished, null));
	}

	void ResetQuest(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Reset, null));
		_totalPoints = 0;
		_timeElapsed = 0;
	}
}
