using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float ReloadTime;
	public float ScaleLimit;
	public int CircleDepth;

	public Transform ShootSpot;
	public Texture ReloadTexture;

	private Transform player;
	private ParticleSystem particle;

	private float _reloadTimeLeft;

	private Vector3 _newShootSpot;

	private bool _weaponActive;
	private bool _questStared;
	private bool _questFinished;
	private bool _reloading;
	private bool _firstHit;
	private bool _goToSpot;
	
	private Ray _ray;
	private RaycastHit _hit;
	private Transform _cameraTransform;

	private Rect fireButtonBoundsDraw;
	private Rect fireButtonBoundsInput;
	public Texture fireButtonTexture;

	void Start () {
	
		_reloadTimeLeft = ReloadTime;
		_questStared = false;
		_firstHit = false;
		_goToSpot = false;
		fireButtonBoundsDraw = new Rect(Screen.width*0.1f, Screen.height*0.9f - Screen.width*0.1f, Screen.width*0.1f, Screen.width*0.1f);
		fireButtonBoundsInput = new Rect(Screen.width*0.1f, Screen.height*0.1f, Screen.width*0.1f, Screen.width*0.1f);
		EventManager.QuestEvent +=  new QuestHandler(ShootQuestRespons);
//		EventManager.OnTouchEvent += TouchRespons;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		particle = GameObject.Find("Smoke").GetComponent("ParticleSystem") as ParticleSystem;
		if(particle != null){
			particle.Stop();
			particle.Clear();
		}
		if(Application.loadedLevel == 3){
			ShootSpot = GameObject.Find("ShootSpot").transform;
		}
	}

	void OnGUI(){
		if(_weaponActive){
			GUI.DrawTexture(fireButtonBoundsDraw, fireButtonTexture);
			if(_reloading)
			{
				float w = ((ReloadTime - _reloadTimeLeft) / ReloadTime) * 200;
				GUI.DrawTexture(new Rect(Screen.width/2 - 100, Screen.height/1.1f, w, 15), ReloadTexture);
			}
		}
	}

	void Update () {

		if(_questStared && _goToSpot){
			_newShootSpot = new Vector3(ShootSpot.transform.position.x, player.position.y, ShootSpot.transform.position.z);
			MovePlayerToShootSpot();
		}
		if(_weaponActive && !_reloading){
			if(Input.GetMouseButtonDown(0)){
				if(fireButtonBoundsInput.Contains(Input.mousePosition))
				{
					Debug.Log("Mouse Shoot!");
					RayCastChecker();
					if(particle != null){
						particle.Play ();
					}
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
	//Clean up later!
	void ShootQuestRespons(object o, QuestEventArgs e){
		if(e.MiniGames == MiniGamesEnum.Musköt){
			if(e.QuestType == QuestTypeEnum.Started){
				Debug.Log("Shoot Started");
				_goToSpot = true;
				_questStared = true;
				_firstHit = false;
			}
			else if(e.QuestType == QuestTypeEnum.Finnished){
				Debug.Log("Shoot Fin");
				_questStared = false;
				DisableWeapon();
			}
			else if(e.QuestType == QuestTypeEnum.Reset){
				Debug.Log("Shoot Reset");
				_firstHit = false;
			}
			Debug.Log("Shoot Done");
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
		direction = player.TransformDirection ( direction.normalized );

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
		QuestEventArgs qEvArgs = new QuestEventArgs(MiniGamesEnum.Musköt ,QuestTypeEnum.OnGoing, infoToSend);
		EventManager.OnQuestEvent(qEvArgs);
	}

	//Should move the player to the spot where he is going to shoot from.
	void MovePlayerToShootSpot(){
		float distToShootSpot = 0;
		float rate = 0;
		float speed = 2;

		distToShootSpot = Vector3.Distance(player.position, _newShootSpot);
		rate = Time.deltaTime * speed;

		if(distToShootSpot <= 0.01f){
			_goToSpot = false;
		}
		if(_goToSpot){
			player.position = Vector3.MoveTowards(player.position, _newShootSpot, rate);
		}
	
		if(!_goToSpot){
			EnableWeapon();
		}
	}
}