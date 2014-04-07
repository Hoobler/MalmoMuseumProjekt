using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

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

	private bool questActive = false;

	public override void TriggerStart ()
	{
		Debug.Log ("Canon quest started");

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
	}

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
		}
	}

	private void Fire(){
		smoke.Stop ();
		smoke.Clear ();
		canonballs_shot++;
		canonball_in_air = true;
		canonBall = Instantiate (Resources.Load ("CanonBall")) as GameObject;
		canonBall.transform.position = canonMuzzle.transform.position;
		canonBall.rigidbody.AddForce ((canonMuzzle.transform.position - canonBase.transform.position).normalized * 3000f);
		smoke.transform.position = canonMuzzle.transform.position;
		smoke.Play ();
	}

	private void UpdateCanonballs(){
		reload_timer += Time.deltaTime;

		if (reload_timer >= RELOAD_TIME) {
			Destroy(canonBall);	
			reload_timer = 0;
			canonball_in_air = false;
		}
	}

	public void CanonBallTrigger(bool hit){
		if (hit) {
			nr_of_hits++;
			UpdateShip();
		}
	}

	private void UpdateShip(){
		ShipScript script = ship.GetComponent (typeof(ShipScript)) as ShipScript;
		if (script != null) {
			script.Speed = new Vector3 (0.03f, -0.02f, 0.03f);
			Debug.Log ("Ship script changed");
			script.hit = true;
		}

	}

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
