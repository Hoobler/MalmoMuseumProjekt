using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	private ParticleSystem particle;
	
	private bool _weaponActive;
	private bool _questStared;

	//Ray casting variables
	private Ray _ray;
	private RaycastHit _hit;
	//Camera transform
	private Transform _cameraTransform;

	void Start () {
		_questStared = false;
		EventManager.OnQuest += EventRespons;
		particle = GameObject.Find("Smoke").GetComponent("ParticleSystem") as ParticleSystem;
		particle.Stop();
		particle.Clear();
	}

	void Update () {
		if(_weaponActive){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log("SHIT!");
				particle.Play ();
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

	void EventRespons(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.Started){
				_questStared = true;
			}
			else if(evArgs.QuestType == QuestTypeEnum.Finnished){
				_questStared = false;
			}
			else if(evArgs.Info == "EnterShootArea" && _questStared){
				EnableWeapon();
			}
			else if (evArgs.Info == "ExitShootArea" && _questStared){
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
				EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.OnGoing, "Hit"));
			}
		}
	}
}
