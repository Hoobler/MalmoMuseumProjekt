using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	private ChangeTargetColor script;
	private float moveSpeed = 5;
	private Vector3 moveToPos;
	private bool locked;
	private bool reachedTarget;

	void Start () {
		EventManager.OnLock += PlayerLock;
	}

	void Update () {
		if(locked && !reachedTarget){
			MovePlayerToPos();
		}

		if(Vector3.Distance(transform.position, moveToPos) <= 0.2f){
			Debug.Log("Stopp!");
			reachedTarget = true;
		}

		RaycastHit hit;
		Transform cam = Camera.main.transform;
		Ray ray = new Ray(cam.position, cam.forward);

		if(Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(ray ,out hit, 100f)){
				Debug.Log("Hit!" + hit.collider.gameObject);
				GameObject otherObj = hit.collider.gameObject;
				if(otherObj.tag == "Target"){
					EventManager.TriggerOnHit(1);
				}
			}
		}
 	}

	void PlayerLock(string type, LockPlayerEventArgs evtArgs){
		if(type == "LockAndLook"){
			locked = true;
			Debug.Log("Player Pos: " + transform.position);
			Debug.Log("Point to lock to: " + evtArgs.transform.position);
			moveToPos = evtArgs.transform.position;
			RotatePlayer(evtArgs.lookAt);
//			gameObject.transform.parent.gameObject.GetComponent<MouseLook>().enabled = false;
		}
		else if (type == "Unlock"){
			locked = false;
			gameObject.transform.parent.gameObject.GetComponent<MouseLook>().enabled = true;
		}
	}

	void MovePlayerToPos(){
		transform.position = Vector3.Slerp(transform.position, moveToPos, moveSpeed * Time.deltaTime);
	}

	//Make this smooth at a later date!
	void RotatePlayer(Transform target){
		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;
	}
}
