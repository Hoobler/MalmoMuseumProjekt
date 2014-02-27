using UnityEngine;
using System.Collections;

public class ChangeTargetColor : MonoBehaviour {

	public float speed = 10f;
	public Vector3 endPos;

	private bool hit = false;
	private bool reachedTarget = false;

	void Start () {
		EventManager.OnHit += ChangeSize;
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

	public void ChangeSize(int id){
		if(id == 1){
		Debug.Log("ChangeSize");
		hit = true;
		transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
}
