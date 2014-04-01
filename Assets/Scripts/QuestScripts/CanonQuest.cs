using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 3;
	private int canonballs_shot;

	private const float ROTATION_SPEED = 20;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject canonPipe;
	GameObject ship;
	GameObject player;


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

		mainCamera.camera.enabled = false;
		canonCamera.camera.enabled = true;

		canonballs_shot = 0;

		questActive = true;

		//player.SetActive (false);

		InitScreenComponents ();

	}

	public override void TriggerFinish ()
	{
		mainCamera.camera.enabled = true;
		canonCamera.camera.enabled = false;
		//player.SetActive (true);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (questActive) {
			if (Input.GetKey (KeyCode.A))
				canon.transform.Rotate(-Vector3.up * ROTATION_SPEED * Time.deltaTime); 
			if (Input.GetKey (KeyCode.D))
				canon.transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
			if (Input.GetKey (KeyCode.W))
				canonPipe.transform.Rotate(Vector3.left * ROTATION_SPEED * Time.deltaTime); 
			if (Input.GetKey (KeyCode.S))
				canonPipe.transform.Rotate(Vector3.right * ROTATION_SPEED * Time.deltaTime);

		}
	}
}
