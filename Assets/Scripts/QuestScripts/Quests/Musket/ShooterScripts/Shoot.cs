﻿using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float ReloadTime;
	public float ScaleLimit;
	public int CircleDepth;

	public Transform ShootSpot;
	public Texture ReloadTexture;

	private ParticleSystem particle;

	private float _reloadTimeLeft;

	private bool _weaponActive;
//	private bool _questStared;
	private bool _questFinished;
	private bool _reloading;
	private bool _firstHit;
	
	private Ray _ray;
	private RaycastHit _hit;
	private Transform _cameraTransform;

	void Start () {
		_reloadTimeLeft = ReloadTime;
//		_questStared = false;
		_firstHit = false;
		EventManager.OnQuest += EventRespons;
		EventManager.OnTouchEvent += TouchRespons;
		particle = GameObject.Find("Smoke").GetComponent("ParticleSystem") as ParticleSystem;
		if(particle != null){
			particle.Stop();
			particle.Clear();
		}
		if(Application.loadedLevel == 2){
			ShootSpot = GameObject.Find("ShootSpot").transform;
		}
	}

	void OnGUI(){
		if(_weaponActive && _reloading){
			float w = ((ReloadTime - _reloadTimeLeft) / ReloadTime) * 200;
			GUI.DrawTexture(new Rect(Screen.width/2 - 100, Screen.height/1.1f, w, 15), ReloadTexture);
		}
	}

	void Update () {
		if(_weaponActive && !_reloading){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log("Mouse Shoot!");
				RayCastChecker();
				if(particle != null){
					particle.Play ();
				}
			}
//			if(Input.touchCount > 0){
//				int i = 0;
//				while (i++ < Input.touchCount){
//					if(Input.GetTouch(i).phase == TouchPhase.Ended){
//						RayCastChecker();
//					}
//				}
//			}
		}

		if(_reloadTimeLeft <= 0){
			_reloading = false;
			_reloadTimeLeft = ReloadTime;
		}else{
			_reloadTimeLeft -= Time.deltaTime;
		}
 	}

	void EventRespons(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			Debug.Log("Shoot test");
			if(evArgs.QuestType == QuestTypeEnum.Started){
				StartCoroutine(MovePlayerToShootSpot());
//				_questStared = true;
				_firstHit = false;
			}
			else if(evArgs.QuestType == QuestTypeEnum.Finnished){
//				_questStared = false;
				DisableWeapon();
			}
			else if(evArgs.QuestType == QuestTypeEnum.Reset){
				_firstHit = false;
			}
		}
	}

	void TouchRespons(TouchEnum touchEnum){
		if (touchEnum == TouchEnum.Touched) {
			if(_weaponActive && !_reloading){
				Debug.Log("Touched Event in Shoot.CS");
				RayCastChecker();
				if(particle != null){
					particle.Play ();
				}
			}
		}
	}

	void EnableWeapon(){
		Debug.Log("Enable Weapon");
		_weaponActive = true;
		EventManager.TriggerOnActivate("CrossHair", ActiveEnum.Active);
	}

	void DisableWeapon(){
		GameObject.Find ("Quest_Handler").GetComponent<QuestManager> ().QuestFinished (); //gör att quest kan triggas igen
		_weaponActive = false;
		EventManager.TriggerOnActivate("CrossHair", ActiveEnum.Disabled);
		Debug.Log("Disable Weapon");
	}

	void RayCastChecker(){
		if(!_firstHit){
			_firstHit = true;
			SendToMusketQuest("FirstHit");
		}
		_reloading = true;
		Vector3 direction = Random.insideUnitCircle * ScaleLimit;
		direction.z = CircleDepth;
		direction = transform.TransformDirection ( direction.normalized );

		_cameraTransform = Camera.main.transform;
		_ray = new Ray(_cameraTransform.position, direction);

		if(Physics.Raycast(_ray ,out _hit, 100f, 1 << 10)){
			//Debug.DrawLine (_cameraTransform.position, _hit.point, Color.red, 10.0f, false);
//			GameObject otherObj = _hit.collider.gameObject;
			if(_hit.collider.name == "BullsEye"){
				SendToMusketQuest("BullsEye");
			}
			if (_hit.collider.name == "WhiteRing"){
				SendToMusketQuest("WhiteRing");
			}
			if (_hit.collider.name == "RedRing"){
				SendToMusketQuest("RedRing");
			}
		}
	}

	void SendToMusketQuest(string infoToSend){
		EventManager.TriggerOnQuest(MiniGamesEnum.Musköt, new QuestEventArgs(QuestTypeEnum.OnGoing, infoToSend));
	}

	//Should move the player to the spot where he is going to shoot from.
	IEnumerator MovePlayerToShootSpot(){
		float distToShootSpot = 0;
		float rate = 0;
		float speed = 2;
		bool goToSpot = true;
		
		Vector3 newShootSpot = new Vector3(ShootSpot.transform.position.x, transform.position.y, ShootSpot.transform.position.z);
		while(goToSpot){
			distToShootSpot = Vector3.Distance(transform.position, newShootSpot);
			rate = Time.deltaTime * speed;
			
			if(distToShootSpot <= 0.01f){
				goToSpot = false;
			}
			if(goToSpot){
				this.transform.position = Vector3.MoveTowards(transform.position, newShootSpot, rate);
				yield return null;
			}
		}

		EventManager.TriggerDisableAndroid("lock");
		EnableWeapon();
	}
}