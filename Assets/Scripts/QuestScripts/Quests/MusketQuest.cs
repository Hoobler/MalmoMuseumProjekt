using UnityEngine;
using System.Collections;

[System.Serializable]
public class Points{
	public int BullsEye;
	public int SecondRing;
	public int ThirdRing;
}

public class MusketQuest : MonoBehaviour {

	public int HitsToFinish;
	public float TimeLimit;
	public Points Points;

	private bool _questSuccess;
	private bool _questStarted;
	private bool _questEnded;
	private bool _firstHit;
	private bool _timeLimitExceeded;
	private int _totalPoints;
	private float _timeElapsed;
	
	void Start () {
		EventManager.OnQuest += EventRespons;

		_questEnded = false;
		_questStarted = false;
		_firstHit = false;
		_questSuccess = false;
		_totalPoints = 0;
	}

	void Update () {
		if(_firstHit && !_questEnded){
			//Ändra till time limit!
			if(_totalPoints >= HitsToFinish && !_timeLimitExceeded){
				_questEnded = true;
				_questSuccess = true;
				QuestFinished();
			}
			if(_questStarted && !_questEnded){
				_timeElapsed += Time.deltaTime;
			}
			if(_timeElapsed >= TimeLimit){
				_timeLimitExceeded = true;
				_questEnded = true;
				_questSuccess = false;
				QuestFinished();
			}
		}
	}

	void OnGUI(){
		if (_questStarted) {
			GUI.Label (new Rect (70, 30, 30, 20), "Poäng: " + _totalPoints.ToString());
			GUI.Label (new Rect (70, 50, 30, 20), _timeElapsed.ToString());
		}
	}

	void EventRespons(MiniGamesEnum miniEnum, QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.OnGoing){
				if(evArgs.Info == "RedRing" && _questStarted){
					//Poäng ska läggas till här!
					Debug.Log("RedRing hit!");
				}
			}
			if(evArgs.QuestType == QuestTypeEnum.Started){
				_questStarted = true;
			}
		}
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
