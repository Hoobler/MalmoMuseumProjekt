using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 10;
	private int canonballs_shot;

	private const float ROTATION_SPEED = 10;
	private Vector3 start_side_rotation;
	private Vector3 start_up_rotation;

	private const float RELOAD_TIME = 5f;
	private float reload_timer;

	private bool canonball_in_air = false;

	private int nr_of_hits = 0;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject canonPipe;
	GameObject ship;
	GameObject player;
	GameObject canonBall;
	GameObject canonMuzzle;
	GameObject canonBase;

	private bool questActive = false;

	public override void TriggerStart ()
	{
		Debug.Log ("Canon quest started");

		mainCamera 	= GameObject.Find ("Main Camera");
		canonCamera = GameObject.Find ("CanonCamera");
		ship 		= GameObject.Find ("Ship");
		player 		= GameObject.FindGameObjectWithTag ("Player");

		canon 		= GameObject.FindGameObjectWithTag ("Kanon");
		canonPipe 	= GameObject.FindGameObjectWithTag ("KanonPipa");
		canonMuzzle = GameObject.Find ("KanonMynning");
		canonBase	= GameObject.Find ("KanonBas");
		

		mainCamera.SetActive(false);
		canonCamera.SetActive (true);

		canonballs_shot = 0;

		questActive = true;
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
		canonballs_shot++;
		canonball_in_air = true;
		canonBall = Instantiate (Resources.Load ("CanonBall")) as GameObject;
		canonBall.transform.position = canonMuzzle.transform.position;
		canonBall.rigidbody.AddForce ((canonMuzzle.transform.position - canonBase.transform.position).normalized * 3000f);
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
		if (hit)
			nr_of_hits++;
	}
}
