using UnityEngine;
using System.Collections;

public class DiceQuest : QuestBase {

	enum State{
		SELECT_REROLL,
		POSTTHROW,
		PRETHROW,
		FINISH
	}

	bool questActive;

	GameObject[] dice;
	State state;
	int numberOfDiceToThrow;

	ArrayList listOfNumbers = new ArrayList();

	bool holdingDownMouseButton;
	Vector3 startHold;
	Vector3 endHold;
	int totalPoints;
	int nrOfRerolls;
	GameObject mainCamera;
	GameObject dicecamera;
	GameObject invisWall;
	GameObject diceparent;

	GUITexture resetButton;

	Transform player;
	// Use this for initialization
	void Start () {
		dicecamera = GameObject.Find ("DiceCamera");
		dicecamera.camera.enabled = false;

	}

	public override void TriggerStart()
	{
		Debug.Log ("START");
		GameObject button = (GameObject)Instantiate (Resources.Load ("ResetButton"));
		resetButton = (GUITexture)button.GetComponent ("GUITexture");
		resetButton.enabled = false;
		mainCamera 	= GameObject.Find ("Main Camera");
		mainCamera.camera.enabled = false;
		dicecamera.camera.enabled = true;
		numberOfDiceToThrow = 5;
		state = State.PRETHROW;

		dice = new GameObject[numberOfDiceToThrow];
			Destroy(GameObject.Find("DiceParent"));

		diceparent = new GameObject ("DiceParent");
		for (int i = 0; i < numberOfDiceToThrow; i++) {
			listOfNumbers.Add(i);
		}
		nrOfRerolls = 5;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		questActive = true;

	}

	public override void TriggerFinish()
	{
		questActive = false;
		mainCamera.camera.enabled = true;
		dicecamera.camera.enabled = false;
		Destroy (resetButton.gameObject);
	}

	int CheckWhichSideIsUp(Transform die)
	{
		int sideUp = 0;
		float angle = 360;

		if(Vector3.Angle (die.up, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.up, Vector3.up);
			sideUp = 6;
		}
		if(Vector3.Angle (-die.up, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.up, Vector3.up);
			sideUp = 1;
		}
		if(Vector3.Angle (die.forward, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.forward, Vector3.up);
			sideUp = 5;
		}
		if(Vector3.Angle (-die.forward, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.forward, Vector3.up);
			sideUp = 2;
		}
		if(Vector3.Angle (die.right, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.right, Vector3.up);
			sideUp = 4;
		}
		if(Vector3.Angle (-die.right, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.right, Vector3.up);
			sideUp = 3;
		}
		return sideUp;
	}

	void PostThrowUpdate()
	{
		int nrOfSleepingDice = 0;
		for (int i = 0; i < dice.Length; i++) {
			if(dice[i].rigidbody.velocity == Vector3.zero)
				nrOfSleepingDice++;
		}
		if (nrOfSleepingDice >= dice.Length) {
			for(int i = 0; i < dice.Length; i++)
			{
				totalPoints += CheckWhichSideIsUp(dice[i].transform);
			}
			if(nrOfRerolls > 0)
			{
				nrOfRerolls--;
				Destroy (invisWall);
				resetButton.enabled = true;
				state = State.SELECT_REROLL;
			}
			else
			{
				state = State.FINISH;
			}
		}

	}

	void FinishUpdate()
	{
		Debug.Log ("" + totalPoints);
		TriggerFinish ();

	}

	void PreThrowUpdate()
	{
		if (!holdingDownMouseButton)
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				holdingDownMouseButton = true;
				startHold = Input.mousePosition;
			}
		}
		else {
			if(!Input.GetMouseButton(0))
			{
				holdingDownMouseButton = false;
				endHold = Input.mousePosition;
				//startHold.y = dicecamera.transform.position.y;
				//endHold.y = startHold.y;
				totalPoints = 0;
				//invisWall = GameObject.Instantiate(Resources.Load("Invisible Walls"), new Vector3(5.29764f, 4.712706f, -36.91311f), new Quaternion()) as GameObject;

				state = State.POSTTHROW;
				startHold = dicecamera.camera.ScreenPointToRay(startHold).origin;
				endHold = dicecamera.camera.ScreenPointToRay(endHold).origin;
				endHold.y = startHold.y;
				Vector3 direction = (endHold  - startHold);

				Debug.Log(startHold.ToString());

				for(int i = 0; i < numberOfDiceToThrow; i++)
				{
					dice[(int)listOfNumbers[i]] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;
					dice[(int)listOfNumbers[i]].transform.position = startHold+new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.5f, -0.1f), Random.Range(-0.1f, 0.1f));

					dice[(int)listOfNumbers[i]].transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					dice[(int)listOfNumbers[i]].rigidbody.angularVelocity = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					dice[(int)listOfNumbers[i]].rigidbody.AddForce(direction*400f);
					dice[(int)listOfNumbers[i]].transform.parent = diceparent.transform;

				}
				numberOfDiceToThrow = 0;
				listOfNumbers.Clear();

			}
		}
	}

	void SelectReRollUpdate()
	{
		if (Input.GetMouseButtonDown (0))
		{

			Ray ray = dicecamera.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			for(int i = 0; i < dice.Length; i++)
			{
				if(dice[i] == hit.collider.gameObject)
				{
					numberOfDiceToThrow++;
					Destroy(dice[i]);
					listOfNumbers.Add(i);
				}
			}


			if(resetButton.GetScreenRect().Contains(Input.mousePosition) || numberOfDiceToThrow == 5)
			{
				state = State.PRETHROW;
				invisWall = GameObject.Instantiate(Resources.Load("Invisible Walls")) as GameObject;
				resetButton.enabled = false;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (questActive)
		{
			switch(state)
			{
			case State.POSTTHROW:
				PostThrowUpdate();
				break;
			case State.PRETHROW:
				PreThrowUpdate();
				break;
			case State.SELECT_REROLL:
				SelectReRollUpdate();
				break;
			case State.FINISH:
				FinishUpdate();
				break;
			default:
				break;
			}

		}
	}
}
