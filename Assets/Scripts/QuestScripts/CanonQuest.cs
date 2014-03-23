using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 3;
	private int canonballs_shot;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject ship;

	private bool questActive = false;
	

	public override void TriggerStart ()
	{
		Debug.Log ("Canon quest started");

		mainCamera 	= GameObject.Find ("Main Camera");
		canonCamera = GameObject.Find ("CanonCamera");
		canon 		= GameObject.Find ("Canon");
		ship 		= GameObject.Find ("Ship");

		mainCamera.camera.enabled = false;
		canonCamera.camera.enabled = true;

		canonballs_shot = 0;

		questActive = true;

		InitScreenComponents ();

	}

	public override void TriggerFinish ()
	{
		mainCamera.camera.enabled = true;
		canonCamera.camera.enabled = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float speed = 20f;
		if (questActive) {
			if (Input.GetKeyDown (KeyCode.A))
				canon.transform.Rotate (Vector3.up * speed * Time.deltaTime);
			if (Input.GetKeyDown (KeyCode.D))
				canon.transform.Rotate (-Vector3.up * speed * Time.deltaTime);
		}
	}

	void InitScreenComponents(){


	}
}
