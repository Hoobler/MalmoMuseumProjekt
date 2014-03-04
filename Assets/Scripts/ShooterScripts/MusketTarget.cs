using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float speed = 2f;
	public Transform endPos;
	public int targetID;
	
	private bool hit = false;
	private bool questTrigger = false;
	private bool reachedTarget = false;

	void Start () {
		EventManager.OnHit += EventRespons;
	}
	
	void Update(){
		if(hit && !reachedTarget){
			StartCoroutine(MoveTarget(transform, transform.position, endPos.position, speed));
		}
		//To stop it from updating and moving the target!
		if(Vector3.Distance(transform.position, endPos.position) <= 0.1f && !reachedTarget){
			Debug.Log("Stopp!");
			reachedTarget = true;
		}

		if(hit && !questTrigger){
			EventManager.TriggerOnQuest(QuestTypeEnum.OnGoing, "stuff");
		}
		if(questTrigger && reachedTarget){
			EventManager.TriggerOnQuest(QuestTypeEnum.GoalReached, "stuff");
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
	
	void EventRespons(int id){
		if(targetID == id){
			Debug.Log("ChangeSize");
			hit = true;
		}
	}
}