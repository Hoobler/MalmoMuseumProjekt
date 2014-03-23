using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 3;
	private int canonballs_shot;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject ship;
	

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
		
	}

	void InitScreenComponents(){


	}
}
