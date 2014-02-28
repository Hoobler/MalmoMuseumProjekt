using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float speed = 10f;
	public Vector3 endPos;
	public int targetID;
	
	private bool hit = false;
	private bool reachedTarget = false;

	void Start () {
		EventManager.OnHit += EventRespons;
	}
	
	void Update(){
		if(hit && !reachedTarget){
			transform.position = Vector3.Slerp(transform.position, endPos, speed * Time.deltaTime);
		}
		//To stop it from updating and moving the target!
		if(Vector3.Distance(transform.position, endPos) <= 0.1f){
			Debug.Log("Stopp!");
			reachedTarget = true;
		}
	}
	
	void EventRespons(int id){
		if(targetID == id){
			Debug.Log("ChangeSize");
			hit = true;
		}
	}
}