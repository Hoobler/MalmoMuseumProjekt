using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float Speed = 2f;
	public Transform[] triggers;
	public int targetID;
	
	private bool _hit = false;
	private bool _questTrigger = false;
	private bool _reachedTarget = false;
	private bool _questRunning;

	void Start () {
		_questRunning = false;
		EventManager.OnQuest += EventRespons;
		StartCoroutine(MoveTarget(triggers[0].position, triggers[1].position));
	}

	// Tabort detta med kanske!?
	void Update(){
		if(_hit && !_questTrigger){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.OnGoing, "stuff"));
		}
		if(_questTrigger && _reachedTarget){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.GoalReached, "stuff"));
		}
	}

	//New ska stoppa target från att röra sig när questen inte är aktiv
	//Resten av tiden sak den åka mellan leftPos och rightPos
	IEnumerator MoveTarget(Vector3 leftPos, Vector3 rightPos){
		float distLeft, distRight;
		float rate = 0;
		bool goLeft = false;
		bool goRight = true;
		Vector3 newLeft = new Vector3(leftPos.x, transform.position.y, leftPos.z);
		Vector3 newRight = new Vector3(rightPos.x, transform.position.y, rightPos.z);
		while(true){
			if(!_questRunning){
				yield return new WaitForSeconds(0.5f);
			} else if(_questRunning) {
				distLeft = Vector3.Distance(this.transform.position, newLeft);
				distRight = Vector3.Distance(this.transform.position, newRight);
				rate = Time.deltaTime * Speed;

				if(distRight <= 0.01f){
					goLeft = true;
					goRight = false;
				}

				if(distLeft <= 0.01f){
					goLeft = false;
					goRight = true;
				}

				if(goRight){
					this.transform.position = Vector3.MoveTowards(transform.position, newRight, rate);
					yield return null;
				}
				if(goLeft){
					this.transform.position = Vector3.MoveTowards(transform.position, newLeft, rate);
					yield return null;
				}
			}
		}
	}
	
	void EventRespons(MiniGamesEnum miniEnum ,QuestEventArgs eventArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(eventArgs.QuestType == QuestTypeEnum.Started){
				_questRunning = true;
			}
			if(eventArgs.QuestType == QuestTypeEnum.Finnished){
				_questRunning = false;
			}
		}
	}
}