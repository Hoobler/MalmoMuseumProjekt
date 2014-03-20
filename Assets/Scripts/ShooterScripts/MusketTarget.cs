using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float speed = 2f;
	//public Transform endPos;
	public int targetID;
	
	private bool hit = false;
	private bool questTrigger = false;
	private bool reachedTarget = false;

	void Start () {
		EventManager.OnHit += EventRespons;
		EventManager.OnQuest += EventReset;
	}
	
	void Update(){
//		if(hit && !reachedTarget){
//			StartCoroutine(MoveTarget(transform, transform.position, endPos.position, speed));
//		}
		//To stop it from updating and moving the target!
//		if(Vector3.Distance(transform.position, endPos.position) <= 0.1f && !reachedTarget){
//			Debug.Log("Stopp!");
//			reachedTarget = true;
//		}

		if(hit && !questTrigger){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.OnGoing, "stuff"));
		}
		if(questTrigger && reachedTarget){
			EventManager.TriggerOnQuest(MiniGamesEnum.Musköt , new QuestEventArgs(QuestTypeEnum.GoalReached, "stuff"));
		}
	}

	void ResetTarget(){}

	IEnumerator MoveTarget(Transform mTransform, Vector3 startPos, Vector3 endPos, float time){
		float i = 0f;
		float rate = 1.0f/time;
		while(i < 1.0f){
			i += Time.deltaTime * rate;
			mTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}

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
			hit = true;
		}
	}
}