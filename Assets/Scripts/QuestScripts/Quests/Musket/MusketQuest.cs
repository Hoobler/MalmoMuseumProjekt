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
	
	void Start () {
		EventManager.OnQuest += QuestRespons;
		//_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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
			GUI.Label (new Rect (70, 50, 100, 20), "Tid: " + _timeElapsed.ToString());
			GUI.Label (new Rect (70, 30, 70, 20), "Poäng: " + _totalPoints.ToString());
		}
	}

	void QuestRespons(MiniGamesEnum miniEnum, QuestEventArgs evArgs){
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
				if(evArgs.Info == "FirstHit" && _questStarted){
					_firstHit = true;
				}
			}
			if(evArgs.QuestType == QuestTypeEnum.Started){
				ResetQuest(); //Really start quest...
				//StartCoroutine(MovePlayerToShootSpot());
				_questStarted = true;
			}
		}
	}

	//Should move the player to the spot where he is going to shoot from.
//	IEnumerator MovePlayerToShootSpot(){
//		float distToShootSpot = 0;
//		float rate = 0;
//		float speed = 2;
//		bool goToSpot = true;
//
//		Vector3 newShootSpot = new Vector3(ShootSpot.transform.position.x, transform.position.y, ShootSpot.transform.position.z);
//		while(goToSpot){
//				distToShootSpot = Vector3.Distance(_playerTransform.position, newShootSpot);
//				rate = Time.deltaTime * speed;
//				
//				if(distToShootSpot <= 0.01f){
//					goToSpot = false;
//				}
//				if(goToSpot){
//					this.transform.position = Vector3.MoveTowards(_playerTransform.position, newShootSpot, rate);
//					yield return null;
//				}
//		}
//	}

	void HighScore(){
		//stuffs at a later date!
	}	

	// Add more to this at a later date!
	void QuestFinished(){
		Debug.Log("Finnished");
		
			if (PlayerPrefs.GetInt ("Squest") == 0)
				PlayerPrefs.SetInt ("Squest", 2);
			else if (PlayerPrefs.GetInt ("Squest") == 1)
				PlayerPrefs.SetInt ("Squest", 3);

		_questStarted = false;
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Finnished, null));
		GameObject endDiag = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		GUIText endText = (GUIText)endDiag.GetComponentInChildren (typeof(GUIText));
		endText.text = "Du fick " + _totalPoints + " poäng!";
	}

	void ResetQuest(){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.Reset, null));
		_totalPoints = 0;
		_timeElapsed = 0;
		_firstHit = false;
		_questEnded = false;
	}
}
