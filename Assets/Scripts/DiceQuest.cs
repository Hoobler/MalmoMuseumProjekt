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
	State state = State.PRETHROW;
	int numberOfDiceToThrow;

	ArrayList listOfNumbers = new ArrayList();

	bool holdingDownMouseButton;
	Vector3 startHold;
	Vector3 endHold;
	int totalPoints;
	int nrOfRerolls;
	GameObject prevMainCamera;
	GameObject dicecamera;

	Transform player;
	// Use this for initialization
	void Start () {

	}

	public override void TriggerStart()
	{
		Debug.Log ("START");
		prevMainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		dicecamera = GameObject.Find ("DiceCamera");
		prevMainCamera.tag = "Untagged";
		dicecamera.tag = "MainCamera";
		numberOfDiceToThrow = 5;

		dice = new GameObject[numberOfDiceToThrow];
		for (int i = 0; i < numberOfDiceToThrow; i++) {
			listOfNumbers.Add(i);
		}
		nrOfRerolls = 1;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		questActive = true;
	}

	public override void TriggerFinish()
	{
		Debug.Log ("FINISH");
		questActive = false;
		prevMainCamera.tag = "MainCamera";
		dicecamera.tag = "Untagged";
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
			if(dice[i].rigidbody.IsSleeping())
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
				totalPoints = 0;
				state = State.POSTTHROW;

				for(int i = 0; i < numberOfDiceToThrow; i++)
				{
					dice[(int)listOfNumbers[i]] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;
					dice[(int)listOfNumbers[i]].transform.position = dicecamera.transform.position;
					dice[(int)listOfNumbers[i]].rigidbody.AddForce(endHold - startHold);
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
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

			if(Input.mousePosition.x < 50 && Input.mousePosition.y < 50)
				state = State.PRETHROW;
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
