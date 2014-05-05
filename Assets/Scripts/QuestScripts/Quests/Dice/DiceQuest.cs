using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

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

	GUIText informationsText;

	GUITexture resetButtonBack;
	GUIText resetButtonText;
	LineRenderer lineRenderer;
	GameObject reminder;
//	Transform player;
	// Use this for initialization
	void Start () {
		dicecamera = GameObject.Find ("DiceCamera");
		dicecamera.camera.enabled = false;
		invisWall = GameObject.Find ("Invisible Walls");
		invisWall.SetActive (false);
		reminder = (GameObject)Instantiate (Resources.Load ("ReminderText"));
		reminder.transform.parent = dicecamera.transform.parent;
	}

	public override void TriggerStart()
	{

		GameObject button = (GameObject)Instantiate (Resources.Load ("ResetButton"));
		resetButtonBack = (GUITexture)button.GetComponent ("GUITexture");
		resetButtonBack.pixelInset = new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.15f, Screen.height * 0.15f);
		resetButtonBack.enabled = false;
		resetButtonText = (GUIText)button.GetComponent ("GUIText");
		resetButtonText.pixelOffset = resetButtonBack.GetScreenRect ().center;
		resetButtonText.fontSize = 14 * (Screen.width / 800);
		resetButtonText.enabled = false;
		mainCamera 	= GameObject.Find ("Main Camera");
		mainCamera.camera.enabled = false;
		dicecamera.camera.enabled = true;

		GameObject informationObj = new GameObject ("Info Text");
		informationObj.transform.parent = dicecamera.transform.parent;
		informationsText = (GUIText)informationObj.AddComponent<GUIText> ();
		informationsText.pixelOffset = new Vector2 (Screen.width * 0.3f, Screen.height * 0.1f);
		informationsText.fontSize = (int)(16 * Screen.width / 800f);
		informationsText.anchor = TextAnchor.MiddleLeft;
		informationsText.alignment = TextAlignment.Left;
		informationsText.text = "Dra över skärmen för att kasta";
		informationsText.color = Color.yellow;

		numberOfDiceToThrow = 5;
		state = State.PRETHROW;
		dice = new GameObject[numberOfDiceToThrow];
			Destroy(GameObject.Find("DiceParent"));

		diceparent = new GameObject ("DiceParent");
		for (int i = 0; i < numberOfDiceToThrow; i++) {
			listOfNumbers.Add(i);
		}
		nrOfRerolls = 5;
//		player = GameObject.FindGameObjectWithTag ("Player").transform;
		questActive = true;
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		((GUITexture)(GameObject.Find ("Karta")).GetComponentInChildren (typeof(GUITexture))).enabled = false;

		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript>()).ChangeText("Dra över skärmen för kasta tärning. Ju längre du drar, desto hårdare kastar du. När du kastat väljer du vilka du ska slå om.");
		//reminderText = (ReminderTextScript)reminder.GetComponent<ReminderTextScript>();
		//reminderText.ChangeText ("Dra över skärmen för kasta tärning. Ju längre du drar, desto hårdare kastar du.");
	}

	public override void TriggerFinish()
	{
		questActive = false;
		mainCamera.camera.enabled = true;
		dicecamera.camera.enabled = false;
		Destroy (informationsText.gameObject);
		Destroy (resetButtonBack.gameObject);
		invisWall.SetActive (false);
		GameObject endDiag = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		GUIText endText = (GUIText)endDiag.GetComponentInChildren (typeof(GUIText));
		endText.text = "Du fick " + totalPoints + " poäng!";
		((GUITexture)(GameObject.Find ("Karta")).GetComponentInChildren (typeof(GUITexture))).enabled = true;
		reminder.SetActive (false);
	}

	public int CheckWhichSideIsUp(Transform die)
	{

		int sideUp = 0;
		float angle = 360;
		
		if(Vector3.Angle (die.up, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.up, Vector3.up);
			//die.up = Vector3.up;
			sideUp = 6;
		}
		if(Vector3.Angle (-die.up, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.up, Vector3.up);
			//die.up = -Vector3.up;
			sideUp = 1;
		}
		if(Vector3.Angle (die.forward, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.forward, Vector3.up);
			//die.forward = Vector3.up;
			sideUp = 5;
		}
		if(Vector3.Angle (-die.forward, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.forward, Vector3.up);
			//die.forward = -Vector3.up;
			sideUp = 2;
		}
		if(Vector3.Angle (die.right, Vector3.up) < angle)
		{
			angle = Vector3.Angle (die.right, Vector3.up);
			//die.right = Vector3.up;
			sideUp = 4;
		}
		if(Vector3.Angle (-die.right, Vector3.up) < angle)
		{
			angle = Vector3.Angle (-die.right, Vector3.up);
			//die.right = -Vector3.up;
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
				invisWall.SetActive(false);
				resetButtonBack.enabled = true;
				resetButtonText.text = "BEHÅLL RESULTAT";
				resetButtonText.enabled = true;
				state = State.SELECT_REROLL;
				informationsText.enabled = true;
				informationsText.text = "Välj de tärningar du vill slå om";
			}
			else
			{
				state = State.FINISH;
			}
		}

	}

	void FinishUpdate()
	{
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
				lineRenderer.enabled = true;
				lineRenderer.SetWidth(0,0.01f);
				lineRenderer.SetPosition(0, dicecamera.camera.ScreenPointToRay(startHold).origin);
				lineRenderer.SetPosition(1, dicecamera.camera.ScreenPointToRay(Input.mousePosition).origin);
			}
		}
		else {
			lineRenderer.SetPosition(0, dicecamera.camera.ScreenPointToRay(startHold).origin);
			lineRenderer.SetPosition(1, dicecamera.camera.ScreenPointToRay(Input.mousePosition).origin);

			if(!Input.GetMouseButton(0))
			{
				holdingDownMouseButton = false;
				endHold = Input.mousePosition;
				//startHold.y = dicecamera.transform.position.y;
				//endHold.y = startHold.y;
				totalPoints = 0;
				//invisWall = GameObject.Instantiate(Resources.Load("Invisible Walls"), new Vector3(5.29764f, 4.712706f, -36.91311f), new Quaternion()) as GameObject;
				lineRenderer.enabled = false;
				state = State.POSTTHROW;
				informationsText.enabled = false;
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
					if(numberOfDiceToThrow == 1)
					{
						resetButtonText.text = "SLÅ OM EN";
					}
					else
						resetButtonText.text = "SLÅ OM "+numberOfDiceToThrow;
				}
			}

			if(resetButtonBack.GetScreenRect().Contains(Input.mousePosition) || numberOfDiceToThrow == 5)
			{
				if(numberOfDiceToThrow == 0)
					state = State.FINISH;
				else
				{
					state = State.PRETHROW;
					invisWall.SetActive(true);
					informationsText.text = "Dra över skärmen för att kasta";
				}
				resetButtonBack.enabled = false;
				resetButtonText.enabled = false;
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
