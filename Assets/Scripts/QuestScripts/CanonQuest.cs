using UnityEngine;
using System.Collections;

public class CanonQuest : QuestBase {

	private const int TOTAL_CANONBALLS = 10;
	private int canonballs_shot;
	private const int ANGLES_TURNED_MAX_SIDE = 30;
	private int angles_turned_side = 0;
	private const int ANGLES_TURNED_MAX_UP = 30;
	private int angles_turned_up = 0;

	GameObject mainCamera;
	GameObject canonCamera;
	GameObject canon;
	GameObject ship;
	GameObject mynning;
	GameObject canonBall;

	private bool questActive = false;
	

	public override void TriggerStart ()
	{
		Debug.Log ("Canon quest started");

		mainCamera 	= GameObject.Find ("Main Camera");
		canonCamera = GameObject.Find ("CanonCamera");
		canon 		= GameObject.FindWithTag ("Kanon");
		ship 		= GameObject.Find ("Ship");
		mynning 	= GameObject.FindWithTag ("Mynning");

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
		//ROTATING THE CANON
		if (questActive) {
			if (Input.GetKey (KeyCode.A)){
				if(angles_turned_side > -ANGLES_TURNED_MAX_SIDE){
					canon.transform.eulerAngles = new Vector3(canon.transform.eulerAngles.x, canon.transform.eulerAngles.y - 1, canon.transform.eulerAngles.z);
					angles_turned_side--;
				}
			}
			if (Input.GetKey (KeyCode.D)){
				if(angles_turned_side < ANGLES_TURNED_MAX_SIDE){
					canon.transform.eulerAngles = new Vector3(canon.transform.eulerAngles.x, canon.transform.eulerAngles.y + 1, canon.transform.eulerAngles.z);
					angles_turned_side++;
				}
			}
			if(Input.GetKey(KeyCode.S)){
				if(angles_turned_up < ANGLES_TURNED_MAX_UP){
					canon.transform.eulerAngles = new Vector3(canon.transform.eulerAngles.x + 1, canon.transform.eulerAngles.y, canon.transform.eulerAngles.z);
					angles_turned_up++;
				}
			}
			if(Input.GetKey(KeyCode.W)){
				if(angles_turned_up > 0){
					canon.transform.eulerAngles = new Vector3(canon.transform.eulerAngles.x - 1, canon.transform.eulerAngles.y, canon.transform.eulerAngles.z);
					angles_turned_up--;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				if(canonballs_shot <= TOTAL_CANONBALLS){
					canonballs_shot++;
					Shootcanon();
				}

			}
		}
		//-----------------------
	}

	void Shootcanon(){
		canonBall = Instantiate (Resources.Load ("CanonBall")) as GameObject;
		canonBall.transform.position = mynning.transform.position;
		canonBall.rigidbody.AddForce(0, 1000f, 1000f);

	}

	void InitScreenComponents(){


	}
}
