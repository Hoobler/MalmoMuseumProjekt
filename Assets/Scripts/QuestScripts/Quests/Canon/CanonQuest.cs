using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase  {

	private const int TOTAL_CANONBALLS = 5;
	private int canonballs_shot;

	private const float ROTATION_SPEED = 10;
	private Vector3 start_side_rotation;
	private Vector3 start_up_rotation;

	private const float RELOAD_TIME = 5f;
	private float reload_timer;

	private bool canonball_in_air = false;

	private int nr_of_hits = 0;

	private ParticleSystem smoke;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject canonPipe;
	GameObject ship;
	GameObject player;
	GameObject canonBall;
	GameObject canonMuzzle;
	GameObject canonBase;

	public Texture canonball_texture;
	public Texture reloadbar_texture;
	public Texture arrow_texture;

	private bool questActive = false;

	GUITexture left_arrow;
	GUITexture right_arrow;
	GUITexture up_arrow;
	GUITexture down_arrow;
	
	GUITexture[] guiList = new GUITexture[4];

	//Called when player starts quest
	public override void TriggerStart ()
	{
		mainCamera 	= GameObject.Find ("Main Camera");
		canonCamera = GameObject.Find ("CanonCamera");
		player		= GameObject.FindGameObjectWithTag ("Player");
		canon 		= GameObject.FindGameObjectWithTag ("Kanon");
		canonPipe 	= GameObject.FindGameObjectWithTag ("KanonPipa");
		canonMuzzle = GameObject.Find ("KanonMynning");
		canonBase	= GameObject.Find ("KanonBas");
		smoke 		= GameObject.Find ("CanonSmoke").GetComponent ("ParticleSystem") as ParticleSystem;

		ship.SetActive (true);

		mainCamera.SetActive(false);
		canonCamera.SetActive (true);

		canonballs_shot = 0;

		questActive = true;
		if (smoke) {
			smoke.Clear ();
			smoke.Stop ();
		}

		Init ();
	}

	//Called when player finishes quest
	public override void TriggerFinish ()
	{

		mainCamera.SetActive (true);
		canonCamera.SetActive (false);
		questActive = false;
		Debug.Log ("Träffar: " + nr_of_hits);
	}

	// Use this for initialization
	void Start () {
		ship = GameObject.Find ("Ship");
		ship.SetActive (false);
	}

	//Initializes arrows on screen
	void Init(){
		GameObject la = new GameObject ();
		left_arrow = (GUITexture)la.AddComponent (typeof(GUITexture));
		left_arrow.texture = arrow_texture;
		left_arrow.transform.position =  new Vector3 (0.65f, 0.1f, 0);
		left_arrow.transform.localScale = new Vector3 (0.1f, 0.1f, 0);

		GameObject ra = new GameObject ();
		right_arrow = (GUITexture)ra.AddComponent (typeof(GUITexture));
		right_arrow.texture = arrow_texture;
		right_arrow.transform.position =  new Vector3 (0.85f, 0.1f, 0);
		right_arrow.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
		

		GameObject ua = new GameObject ();
		up_arrow = (GUITexture)ua.AddComponent (typeof(GUITexture));
		up_arrow.texture = arrow_texture;
		up_arrow.transform.position =  new Vector3 (0.75f, 0.2f, 0);
		up_arrow.transform.localScale = new Vector3 (0.1f, 0.11f, 0);
		

		GameObject da = new GameObject ();
		down_arrow = (GUITexture)da.AddComponent (typeof(GUITexture));
		down_arrow.texture = arrow_texture;
		down_arrow.transform.position =  new Vector3 (0.75f, 0.1f, 0);
		down_arrow.transform.localScale = new Vector3 (0.1f, 0.1f, 0);

		guiList [0] = left_arrow;
		guiList [1] = right_arrow;
		guiList [2] = up_arrow;
		guiList [3] = down_arrow;

	}
	
	// Update is called once per frame
	void Update () {
		if (questActive) {
			//Sväng vänster
			if (Input.GetKey (KeyCode.A) && start_side_rotation.y > -10f) {
				canon.transform.Rotate(-Vector3.up * ROTATION_SPEED * Time.deltaTime);
				start_side_rotation += -Vector3.up * ROTATION_SPEED * Time.deltaTime;
			}
			//Sväng höger
			if (Input.GetKey (KeyCode.D) && start_side_rotation.y < 10f){
				canon.transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
				start_side_rotation += Vector3.up * ROTATION_SPEED * Time.deltaTime;
			}
			//Sväng upp
			if (Input.GetKey (KeyCode.W) && start_up_rotation.x > -10f){
				canonPipe.transform.Rotate(Vector3.left * ROTATION_SPEED * Time.deltaTime); 
				start_up_rotation += Vector3.left * ROTATION_SPEED * Time.deltaTime;
			}
			//Sväng ner
			if (Input.GetKey (KeyCode.S) && start_up_rotation.x < 0f){
				canonPipe.transform.Rotate(Vector3.right * ROTATION_SPEED * Time.deltaTime);
				start_up_rotation += Vector3.right * ROTATION_SPEED * Time.deltaTime;
			}
			//Skjut
			if(Input.GetKeyDown(KeyCode.Space) && canonballs_shot <= TOTAL_CANONBALLS && !canonball_in_air){
				Fire();
			}
			//Avsluta spel
			if(canonballs_shot >= TOTAL_CANONBALLS)
				TriggerFinish();
			if(canonball_in_air)
				UpdateCanonballs();

			UpdateTouch();
		}

	}

	//Touch input for controlling canon (Android)
	void UpdateTouch(){

		if (Input.touches.Length == 0) {

		} 
		else {
			for(int j = 0; j < 4; j++)
				for (int i = 0; i < Input.touchCount; i++) {
				if (guiList[j] != null && (guiList[j].HitTest (Input.GetTouch (i).position))) {
						//if current touch hits our guitexture, run this code
						if (Input.GetTouch (i).phase == TouchPhase.Began) {
							if(j == 0){
								Debug.Log ("Roterar vänster");
								canon.transform.Rotate(-Vector3.up * ROTATION_SPEED * Time.deltaTime);
								start_side_rotation += -Vector3.up * ROTATION_SPEED * Time.deltaTime;
							}
							if(j == 1){
								Debug.Log("Roterar höger");
							canon.transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
							start_side_rotation += Vector3.up * ROTATION_SPEED * Time.deltaTime;
						}
							if(j == 2){
								Debug.Log("Roterar upp");
							canonPipe.transform.Rotate(Vector3.left * ROTATION_SPEED * Time.deltaTime); 
							start_up_rotation += Vector3.left * ROTATION_SPEED * Time.deltaTime;
						}
							if(j == 3){
								Debug.Log("Roterar ner");
							canonPipe.transform.Rotate(Vector3.right * ROTATION_SPEED * Time.deltaTime);
							start_up_rotation += Vector3.right * ROTATION_SPEED * Time.deltaTime;
						}
						}
						if (Input.GetTouch (i).phase == TouchPhase.Ended) {
						}
				}
			}
		}
	}

	//Called when player presses shoots the canon
	private void Fire(){
		smoke.Stop ();
		smoke.Clear ();
		canonballs_shot++;
		canonball_in_air = true;
		canonBall = Instantiate (Resources.Load ("CanonBall")) as GameObject;
		canonBall.transform.position = canonMuzzle.transform.position;
		canonBall.rigidbody.AddForce ((canonMuzzle.transform.position - canonBase.transform.position).normalized * 4000f);
		smoke.transform.position = canonMuzzle.transform.position;
		smoke.Play ();
	}


	//Updates reload timer and destroys canonball
	private void UpdateCanonballs(){
		reload_timer += Time.deltaTime;

		if (reload_timer >= RELOAD_TIME) {
			Destroy(canonBall);	
			reload_timer = 0;
			canonball_in_air = false;
		}
	}

	//Called from CanonBallScript when it hit's the boat
	public void CanonBallTrigger(bool hit){
		if (hit) {
			nr_of_hits++;
			ShipCollision();
		}
	}

	//Called when ship gets hit
	private void ShipCollision(){
		ShipScript script = ship.GetComponent (typeof(ShipScript)) as ShipScript;
		if (script != null) {
			script.Speed = new Vector3 (0.03f, -0.02f, 0.03f);
			Debug.Log ("Ship script changed");
			script.hit = true;
		}

	}

	//Draws GUI elements
	void OnGUI(){
		if (questActive) {
			int sh = Screen.height;
			for(int i = 0; i < TOTAL_CANONBALLS - canonballs_shot; i++){
				GUI.DrawTexture(new Rect(10 + 30 * i,sh - 70,60,60),canonball_texture, ScaleMode.ScaleToFit);
			}	

			if(canonball_in_air){
				float w = ((RELOAD_TIME - reload_timer) / RELOAD_TIME) * 200;
				GUI.DrawTexture(new Rect(Screen.width/2 - 100,sh/1.1f, w, 15), reloadbar_texture);
			}

		}
	}
}
