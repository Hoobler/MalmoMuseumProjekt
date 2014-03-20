using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float Speed = 2f;
	public Transform[] triggers;
	public int targetID;
	
	private bool _hit = false;
	private bool _questTrigger = false;
	private bool _reachedTarget = false;
	private bool _stopped;

	void Start () {
		EventManager.OnHit += EventRespons;
		EventManager.OnQuest += EventReset;
		StartCoroutine(MoveTarget(triggers[0].position, triggers[1].position));
	}
	
	void Update(){
//		if(hit && !reachedTarget){
//			StartCoroutine(MoveTarget(transform, transform.position, endPos.position, speed));
//		}

		if(_hit && !_questTrigger){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.OnGoing, "stuff"));
		}
		if(_questTrigger && _reachedTarget){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.GoalReached, "stuff"));
		}
	}

	void ResetTarget(){}

	//Old
//	IEnumerator MoveTarget(Transform mTransform, Vector3 startPos, Vector3 endPos, float time){
//		float i = 0f;
//		float rate = 1.0f/time;
//		while(i < 1.0f){
//			if(!
//			i += Time.deltaTime * rate;
//			mTransform.position = Vector3.Lerp(startPos, endPos, i);
//			yield return null;
//		}
//	}

	//New ska stoppa target från att röra sig när questen inte är aktiv
	//Resten av tiden sak den åka mellan leftPos och rightPos
	IEnumerator MoveTarget(Vector3 leftPos, Vector3 rightPos){
		float distLeft, distRight;
		float rate = 0;
		bool goLeft = false;
		bool goRight = true;
		while(true){
			if(_stopped){
				yield return new WaitForSeconds(0.5f);
			} else {
				distLeft = Vector3.Distance(this.transform.position, leftPos);
				distRight = Vector3.Distance(this.transform.position, rightPos);
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
					this.transform.position = Vector3.MoveTowards(transform.position, rightPos, rate);
					yield return null;
				}
				if(goLeft){
					this.transform.position = Vector3.MoveTowards(transform.position, leftPos, rate);
					yield return null;
				}
			}
		}
	}

	//Tabort senare antaligen! Yes Tabort!
	void EventReset(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.Reset){
				ResetTarget();
			}
		}
	}
	
	void EventRespons(int id){
		if(targetID == id){
			Debug.Log("Target hit! " + id);
			_hit = true;
		}
	}
}