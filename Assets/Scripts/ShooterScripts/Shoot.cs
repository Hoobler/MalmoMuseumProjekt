using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	private Vector3 moveToPos;
	private bool _reachedTarget;
	private bool _weaponActive;

	void Start () {
		EventManager.OnQuest += EventRespons;
	}

	void Update () {
		RaycastHit hit;
		Transform cam = Camera.main.transform;
		Ray ray = new Ray(cam.position, cam.forward);

		if(Input.GetMouseButtonDown(0) && _weaponActive){
			if(Physics.Raycast(ray ,out hit, 100f)){
				Debug.Log("Hit!" + hit.collider.gameObject);
				GameObject otherObj = hit.collider.gameObject;
				if(otherObj.tag == "Target"){
					EventManager.TriggerOnHit(1);
				}
			}
		}
 	}

	void EventRespons(string type){
		if(type == "EnterShootArea"){
			EnableWeapon();
		}
		else if (type == "ExitShootArea"){
			DisableWeapon();
		}
	}

	void EnableWeapon(){
		Debug.Log("Enable Weapon");
		_weaponActive = true;
	}

	void DisableWeapon(){
		_weaponActive = false;
		Debug.Log("Disable Weapon");
	}
}
