using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	private bool _weaponActive;

	//Ray casting variables
	private Ray _ray;
	private RaycastHit _hit;
	//Camera transform
	private Transform _cameraTransform;

	void Start () {
		EventManager.OnQuest += EventRespons;
	}

	void Update () {
		if(_weaponActive){
			if(Input.GetMouseButtonDown(0)){
				RayCastChecker();
			}
			if(Input.touchCount > 0){
				int i = 0;
				while (i++ < Input.touchCount){
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
						RayCastChecker();
					}
				}
			}
		}
 	}

	void EventRespons(QuestTypeEnum enumType, string type){
		if(enumType == QuestTypeEnum.Trigger){
			if(type == "EnterShootArea"){
				EnableWeapon();
			}
			else if (type == "ExitShootArea"){
				DisableWeapon();
			}
		}
	}

	void EnableWeapon(){
		Debug.Log("Enable Weapon");
		_weaponActive = true;
		EventManager.TriggerOnActivate("CrossHair", ActiveEnum.Active);
	}

	void DisableWeapon(){
		_weaponActive = false;
		EventManager.TriggerOnActivate("CrossHair", ActiveEnum.Disabled);
		Debug.Log("Disable Weapon");
	}

	void RayCastChecker(){
		_cameraTransform = Camera.main.transform;
		_ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
		
		if(Physics.Raycast(_ray ,out _hit, 100f)){
			Debug.Log("Hit!" + _hit.collider.gameObject);
			GameObject otherObj = _hit.collider.gameObject;
			if(otherObj.tag == "Target"){
				EventManager.TriggerOnHit(1);
			}
		}
	}
}
