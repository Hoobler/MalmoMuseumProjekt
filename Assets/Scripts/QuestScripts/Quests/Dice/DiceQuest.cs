using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class DiceQuest : QuestBase {

	enum State{
		PLAYERPOSTTHROW,
		PLAYERPRETHROW,
		PLAYERCONTINUE,
		OPPONENTPRETHROW,
		OPPONENTPOSTTHROW,
		OPPONENTCONTINUE,
		FINISH
	}

	bool questActive;

	GameObject[] dice;
	State state;
	int numberOfDiceToThrow;

	bool holdingDownMouseButton;
	Vector3 startHold;
	Vector3 endHold;
	int winsPlayer;
	int winsOpponent;
	int winsForSuccess;
	int totalPoints;
	float timer;
	float totalTime;
	GameObject mainCamera;
	GameObject dicecamera;
	GameObject invisWall;
	GameObject diceparent;

	GUIText informationsText;

	GUITexture resetButtonBack;
	GUIText resetButtonText;
	LineRenderer lineRenderer;
	GameObject reminder;
	GameObject endNotification;
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
		endNotification = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
//		GameObject button = (GameObject)Instantiate (Resources.Load ("ResetButton"));
//		resetButtonBack = (GUITexture)button.GetComponent ("GUITexture");
//		resetButtonBack.pixelInset = new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.15f, Screen.height * 0.15f);
//		resetButtonBack.enabled = false;
//		resetButtonText = (GUIText)button.GetComponent ("GUIText");
//		resetButtonText.pixelOffset = resetButtonBack.GetScreenRect ().center;
//		resetButtonText.fontSize = 14 * (Screen.width / 800);
//		resetButtonText.enabled = false;
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

		totalTime = 1.0f;
		numberOfDiceToThrow = 2;
		winsForSuccess = 3;
		winsPlayer = 0;
		winsOpponent = 0;
		totalPoints = 0;
		state = State.OPPONENTPRETHROW;
		dice = new GameObject[numberOfDiceToThrow];
		Destroy(GameObject.Find("DiceParent"));

		diceparent = new GameObject ("DiceParent");

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

	public override void TriggerFinish(bool success)
	{
		base.TriggerFinish (success);

		questActive = false;
		mainCamera.camera.enabled = true;
		dicecamera.camera.enabled = false;
		Destroy (informationsText.gameObject);
//		Destroy (resetButtonBack.gameObject);
		invisWall.SetActive (false);
		//GameObject endDiag = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		//GUIText endText = (GUIText)endDiag.GetComponentInChildren (typeof(GUIText));
		if(success)
			endNotification.GetComponent<endNotificationScript>().Activate("Du vann spelet!");
		else
			endNotification.GetComponent<endNotificationScript>().Activate("Du förlorade spelet =(");
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


	void OpponentContinue()
	{
		if (Input.GetMouseButtonDown (0))
		{
			state = State.PLAYERPRETHROW;
			informationsText.text = "DRA ÖVER SKÄRMEN FÖR ATT KASTA";
			for(int i = 0; i < numberOfDiceToThrow; i++)
				Destroy(dice[i]);
		}
		
	}
	
	void OpponentPostThrow()
	{
		if(timer <= 0)
		{
			int nrOfSleepingDice = 0;
			for (int i = 0; i < dice.Length; i++) {
				if(dice[i].rigidbody.velocity == Vector3.zero)
					nrOfSleepingDice++;
			}
			if (nrOfSleepingDice >= dice.Length) {
				totalPoints = 0;
				for(int i = 0; i < dice.Length; i++)
				{
					totalPoints += CheckWhichSideIsUp(dice[i].transform);
				}
				state = State.OPPONENTCONTINUE;
				
				informationsText.enabled = true;
				informationsText.text = "TRYCK FÖR ATT FORTSÄTTA";
				invisWall.SetActive(false);
			}
		}
		else
			timer -= Time.deltaTime;
	}
	
	void OpponentPreThrow()
	{
		startHold = new Vector3 (Screen.width / 2, Screen.height *0.75f, 0);
		endHold = new Vector3 (Random.Range ((int)(Screen.width*0.1f), (int)(Screen.width*0.9f)), Random.Range (Screen.height/10, Screen.height/2), 0);
		startHold = dicecamera.camera.ScreenPointToRay(startHold).origin;
		endHold = dicecamera.camera.ScreenPointToRay(endHold).origin;
		endHold.y = startHold.y;
		Vector3 direction = endHold - startHold;

		for(int i = 0; i < dice.Length; i++)
		{
			dice[i] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;

			dice[i].transform.position = startHold+new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.5f, -0.1f), Random.Range(-0.1f, 0.1f));

			
			dice[i].transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
			dice[i].rigidbody.angularVelocity = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
			dice[i].rigidbody.AddForce(direction*400f);
			dice[i].transform.parent = diceparent.transform;
		}
		state = State.OPPONENTPOSTTHROW;
		timer = totalTime;
		
		invisWall.SetActive(true);
	}
	void PlayerContinue()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(winsPlayer == winsForSuccess)
			{
				informationsText.text = "DU VANN SPELET";
				TriggerFinish(true);
			}
			else if(winsOpponent == winsForSuccess)
			{
				informationsText.text = "DU FÖRLORADE SPELET";
				TriggerFinish(false);
			}
			else
			{
				for(int i = 0; i < numberOfDiceToThrow; i++)
					Destroy(dice[i]);
				state = State.OPPONENTPRETHROW;
				
				informationsText.enabled = false;
			}

		}
	}

	void PlayerPostThrow()
	{
		if(timer <= 0.0f)
		{
			int nrOfSleepingDice = 0;
			for (int i = 0; i < dice.Length; i++) {
				if(dice[i].rigidbody.velocity == Vector3.zero)
					nrOfSleepingDice++;
			}
			if (nrOfSleepingDice >= dice.Length)
			{
				int playerPoints = 0;
				for(int i = 0; i < dice.Length; i++)
				{
					playerPoints += CheckWhichSideIsUp(dice[i].transform);
				}
				invisWall.SetActive(false);
					
				informationsText.enabled = true;
				if(playerPoints > totalPoints)
				{
					informationsText.text = "DU VANN DENNA RUNDAN";
					Instantiate(Resources.Load("FadeCorrect"));
					winsPlayer++;
				}
				else if(playerPoints == totalPoints)
				{
					informationsText.text = "INGEN VANN, RUNDAN RÄKNAS INTE";
				}
				else
				{
					informationsText.text = "DU FÖRLORADE DENNA RUNDA";
					Instantiate(Resources.Load ("FadeWrong"));
					winsOpponent++;
			
				}
					
				state = State.PLAYERCONTINUE;
				
					
			
			}
		}
		else
			timer -= Time.deltaTime;
	}

	void PlayerPreThrow()
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

				//invisWall = GameObject.Instantiate(Resources.Load("Invisible Walls"), new Vector3(5.29764f, 4.712706f, -36.91311f), new Quaternion()) as GameObject;
				lineRenderer.enabled = false;
				state = State.PLAYERPOSTTHROW;
				
				informationsText.enabled = false;
				startHold = dicecamera.camera.ScreenPointToRay(startHold).origin;
				endHold = dicecamera.camera.ScreenPointToRay(endHold).origin;
				endHold.y = startHold.y;
				Vector3 direction = (endHold  - startHold);
				timer = totalTime;
				//Debug.Log(startHold.ToString());

				for(int i = 0; i < numberOfDiceToThrow; i++)
				{
					dice[i] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;
					dice[i].transform.position = startHold+new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.5f, -0.1f), Random.Range(-0.1f, 0.1f));

					dice[i].transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					dice[i].rigidbody.angularVelocity = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					dice[i].rigidbody.AddForce(direction*400f);
					dice[i].transform.parent = diceparent.transform;

				}

			}
		}
	}
	void FinishUpdate()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(winsPlayer == winsForSuccess)
				TriggerFinish (true);
			else
				TriggerFinish (false);
		}

	}
//	void SelectReRollUpdate()
//	{
//		if (Input.GetMouseButtonDown (0))
//		{
//
//			Ray ray = dicecamera.camera.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			Physics.Raycast(ray, out hit);
//			for(int i = 0; i < dice.Length; i++)
//			{
//				if(dice[i] == hit.collider.gameObject)
//				{
//
//					numberOfDiceToThrow++;
//					Destroy(dice[i]);
//					listOfNumbers.Add(i);
//					if(numberOfDiceToThrow == 1)
//					{
//						resetButtonText.text = "SLÅ OM EN";
//					}
//					else
//						resetButtonText.text = "SLÅ OM "+numberOfDiceToThrow;
//				}
//			}
//
//			if(resetButtonBack.GetScreenRect().Contains(Input.mousePosition) || numberOfDiceToThrow == 5)
//			{
//				if(numberOfDiceToThrow == 0)
//					state = State.FINISH;
//				else
//				{
//					state = State.PRETHROW;
//					invisWall.SetActive(true);
//					informationsText.text = "Dra över skärmen för att kasta";
//				}
//				resetButtonBack.enabled = false;
//				resetButtonText.enabled = false;
//			}
//		}
//	}
	// Update is called once per frame
	void Update () {
		if (questActive)
		{
			switch(state)
			{
			case State.PLAYERPOSTTHROW:
				PlayerPostThrow();
				break;
			case State.PLAYERPRETHROW:
				PlayerPreThrow();
				break;
			case State.OPPONENTPRETHROW:
				OpponentPreThrow();
				break;
			case State.OPPONENTPOSTTHROW:
				OpponentPostThrow();
				break;
			case State.PLAYERCONTINUE:
				PlayerContinue();
				break;
			case State.OPPONENTCONTINUE:
				OpponentContinue();
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
