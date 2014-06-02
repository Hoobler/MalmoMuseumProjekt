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
//	public Transform ShootSpot;
	
	private bool _questStarted;
	private bool _questEnded;
	private bool _firstHit;
	private int _totalPoints;
	private float _timeElapsed;
	private Transform _playerTransform;

	private GameObject endNotification;
	
	void Start () {
//		EventManager.OnQuest += QuestRespons;
		EventManager.QuestEvent += new QuestHandler(QuestRespons);
		//_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		endNotification = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		_questEnded = false;
		_questStarted = false;
		_firstHit = false;
		_totalPoints = 0;
	}

	void Test(object o, QuestEventArgs e){
		Debug.Log("Quest Type: " + e.QuestType);
	}

	void Update () {
		if(_firstHit && !_questEnded){
			if(_questStarted && !_questEnded){
				_timeElapsed += Time.deltaTime;
			}
		}
		if(_timeElapsed >= TimeLimit && !_questEnded){
			_questEnded = true;
			//HighScore();
			QuestFinished();
		}
	}
	
	void OnGUI(){
		if (_questStarted) {
			GUI.Label (new Rect (70, 50, 100, 20), "Tid: " + _timeElapsed.ToString());
			GUI.Label (new Rect (70, 30, 70, 20), "Poäng: " + _totalPoints.ToString());
		}
	}

	void QuestRespons(object o, QuestEventArgs e){
		if(e.MiniGames == MiniGamesEnum.Musköt){
			if(e.QuestType == QuestTypeEnum.OnGoing){
				if(e.Info == "BullsEye" && _questStarted){
					_totalPoints += Points.BullsEye;
				}
				if(e.Info == "RedRing" && _questStarted){
					_totalPoints += Points.ThirdRing;
				}
				if(e.Info == "WhiteRing" && _questStarted){
					_totalPoints += Points.SecondRing;
				}
				if(e.Info == "FirstHit" && _questStarted){
					_firstHit = true;
				}
			}
			if(e.QuestType == QuestTypeEnum.Started){
				Debug.Log("Reset Quest");
				ResetQuest(); //Really start quest...
				_questStarted = true;
			}
			//Test stuff
			if(e.QuestType == QuestTypeEnum.Started){
				Debug.Log("Started info: " + e.Info);
			}
			if(e.QuestType == QuestTypeEnum.Reset){
				Debug.Log("Reset info: " + e.Info);
			}
			if(e.QuestType == QuestTypeEnum.Trigger){
				Debug.Log("Trigger info: " + e.Info);
			}
			if(e.QuestType == QuestTypeEnum.OnGoing){
				Debug.Log("OnGoing info: " + e.Info);
			}
			if(e.QuestType == QuestTypeEnum.GoalReached){
				Debug.Log("GoalReached info: " + e.Info);
			}
			if(e.QuestType == QuestTypeEnum.Finnished){
				Debug.Log("Finnished info: " + e.Info);
			}
		}
	}

	void HighScore(){
		//stuffs at a later date!
	}	

	// Add more to this at a later date!
	void QuestFinished(){
		Debug.Log("Finnished");
		
			if (PlayerPrefs.GetInt ("Squest") == 0)
				PlayerPrefs.SetInt ("Squest", 1);
			else if (PlayerPrefs.GetInt ("Squest") == 2)
				PlayerPrefs.SetInt ("Squest", 3);

		_questStarted = false;

		//The quest is finnished! hurr durr!!
		QuestEventArgs qEvArgs = new QuestEventArgs(MiniGamesEnum.Musköt ,QuestTypeEnum.Finnished);
		EventManager.OnQuestEvent(qEvArgs);
		//Enable Android again!
		AndroidDisableArgs args = new AndroidDisableArgs();
		args.Disable = false;
		EventManager.TriggerDisableAndroid(args);

		endNotification.GetComponent<endNotificationScript> ().Activate ("Grattis du klarade spelet, du fick " + _totalPoints + " poäng!");
	}

	void ResetQuest(){
		Debug.Log("MusketQuest resetQuest");
		AndroidDisableArgs args = new AndroidDisableArgs();
		args.Disable = true;
		EventManager.TriggerDisableAndroid(args);
//		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Reset));
		_totalPoints = 0;
		_timeElapsed = 0;
		_firstHit = false;
		_questEnded = false;
	}
}
