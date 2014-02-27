using UnityEngine;
using System.Collections;

public class ShootSpot : MonoBehaviour {

	public Transform lookAt;
	public bool lockPlayer;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			if(lockPlayer){
				EventManager.TriggerLockPlayer("LockAndLook", new LockPlayerEventArgs(gameObject.transform, lookAt));
			}
		}
	}
}
