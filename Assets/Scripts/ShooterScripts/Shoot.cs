using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float ReloadTime;
	public float ScaleLimit;
	public int CircleDepth;

	public Texture ReloadTexture;

	private ParticleSystem particle;

	private float _reloadTimeLeft;

	private bool _weaponActive;
	private bool _questStared;
	private bool _questFinished;
	private bool _reloading;
	private bool _firstHit;
	
	private Ray _ray;
	private RaycastHit _hit;
	private Transform _cameraTransform;

	void Start () {
		_reloadTimeLeft = ReloadTime;
		_questStared = false;
		_firstHit = false;
		EventManager.OnQuest += EventRespons;
		particle = GameObject.Find("Smoke").GetComponent("ParticleSystem") as ParticleSystem;
		if(particle != null){
			particle.Stop();
			particle.Clear();
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
			if(Input.touchCount > 0){
				int i = 0;
				while (i++ < Input.touchCount){
					if(Input.GetTouch(i).phase == TouchPhase.Ended){
						RayCastChecker();
					}
				}
			}
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
				_questStared = true;
				_firstHit = false;
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
			GameObject otherObj = _hit.collider.gameObject;
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

	//Polish senare!
	void MovePlayerToCenter(){
		//this.transform.position = Vector3.MoveTowards(transform.position, newRight, rate);
	}
}