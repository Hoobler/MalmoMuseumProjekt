using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 10;
	private int canonballs_shot;

	private const float ROTATION_SPEED = 10;
	private Vector3 start_side_rotation;
	private Vector3 start_up_rotation;


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
		canon 		= GameObject.FindGameObjectWithTag ("Kanon");
		canonPipe 	= GameObject.FindGameObjectWithTag ("KanonPipa");
		ship 		= GameObject.Find ("Ship");
		player 		= GameObject.FindGameObjectWithTag ("Player");
		canonMuzzle = GameObject.Find ("KanonMynning");
		canonBase	= GameObject.Find ("KanonBas");
		

		mainCamera.SetActive(false);
		canonCamera.SetActive (true);
		//player.SetActive (false);

		canonballs_shot = 0;

		questActive = true;
	}

	public override void TriggerFinish ()
	{
		mainCamera.SetActive (true);
		canonCamera.SetActive (false);
		//player.SetActive (true);
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
			if(Input.GetKeyDown(KeyCode.Space) && canonballs_shot <= TOTAL_CANONBALLS){
				Fire();
			}

		}
	}

	private void Fire(){
		canonballs_shot++;
		canonBall = Instantiate (Resources.Load ("CanonBall")) as GameObject;
		canonBall.transform.position = canonMuzzle.transform.position;
		canonBall.rigidbody.AddForce ((canonMuzzle.transform.position - canonBase.transform.position).normalized * 3000f);

	}
}
