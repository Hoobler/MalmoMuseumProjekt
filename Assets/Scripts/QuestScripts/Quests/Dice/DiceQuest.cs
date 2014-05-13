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
	GameObject lightparent;

	GUIText informationsText;

	GameObject reminder;
	GameObject endNotification;

	GameObject opponentCounter;
	GameObject playerCounter;
	Vector3 opponentDiceOrigin;
	Vector3 playerDiceOrigin;
	Vector3 playerPos;
//	Transform player;
	// Use this for initialization
	void Start () {
		dicecamera = GameObject.Find ("DiceCamera");
		dicecamera.camera.enabled = false;
		invisWall = GameObject.Find ("Invisible Walls");
		invisWall.SetActive (false);
		reminder = (GameObject)Instantiate (Resources.Load ("ReminderText"));
		reminder.transform.parent = dicecamera.transform.parent;
		playerCounter = (GameObject)Instantiate (Resources.Load ("counters/PlayerCounter"));
		playerCounter.transform.parent = dicecamera.transform.parent;
		opponentCounter = (GameObject)Instantiate (Resources.Load ("counters/OpponentCounter"));
		opponentCounter.transform.parent = dicecamera.transform.parent;
		opponentDiceOrigin = GameObject.Find ("DiceOpponentStart").transform.position;
		playerDiceOrigin = GameObject.Find ("DicePlayerStart").transform.position;
		playerPos = GameObject.Find ("DicePlayerPosition").transform.position;
	}

	public override void TriggerStart()
	{
		playerCounter.SetActive (true);
		playerCounter.GetComponent<GUIText> ().text = "0";
		opponentCounter.SetActive (true);
		opponentCounter.GetComponent<GUIText> ().text = "0";

		endNotification = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
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

		GameObject.FindWithTag ("Player").transform.position = playerPos;

		totalTime = 1.0f;
		numberOfDiceToThrow = 2;
		winsForSuccess = 3;
		winsPlayer = 0;
		winsOpponent = 0;
		totalPoints = 0;
		state = State.OPPONENTPRETHROW;
		dice = new GameObject[numberOfDiceToThrow*2];
		Destroy(GameObject.Find("DiceParent"));

		diceparent = new GameObject ("DiceParent");
		Destroy (GameObject.Find("LightParent"));
		lightparent = new GameObject("LightParent");
		questActive = true;
		((GUITexture)(GameObject.Find ("Karta")).GetComponentInChildren (typeof(GUITexture))).enabled = false;

		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript>()).ChangeText("Dra över skärmen för kasta tärning. Ju längre du drar, desto hårdare kastar du. När du kastat väljer du vilka du ska slå om.");
	}

	public override void TriggerFinish(bool success)
	{
		base.TriggerFinish (success);

		questActive = false;
		mainCamera.camera.enabled = true;
		dicecamera.camera.enabled = false;
		Destroy (informationsText.gameObject);
		Destroy(lightparent);
		invisWall.SetActive (false);
		if(success)
			endNotification.GetComponent<endNotificationScript>().Activate("Du vann spelet!");
		else
			endNotification.GetComponent<endNotificationScript>().Activate("Du förlorade spelet =(");
		((GUITexture)(GameObject.Find ("Karta")).GetComponentInChildren (typeof(GUITexture))).enabled = true;
		reminder.SetActive (false);
		playerCounter.SetActive (false);
		opponentCounter.SetActive (false);

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

		}
		
	}
	
	void OpponentPostThrow()
	{
		if(timer <= 0)
		{
			int nrOfSleepingDice = 0;
			for (int i = 0; i < dice.Length/2; i++) {
				if(dice[i].rigidbody.velocity == Vector3.zero)
					nrOfSleepingDice++;
			}
			if (nrOfSleepingDice >= dice.Length/2) {
				totalPoints = 0;
				for(int i = 0; i < dice.Length/2; i++)
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

		endHold = playerDiceOrigin + new Vector3 (Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));

		endHold.y = opponentDiceOrigin.y;
		Vector3 direction = endHold - opponentDiceOrigin;
		if(direction.magnitude < 0.3f)
			direction = direction.normalized*0.3f;

		for(int i = 0; i < dice.Length/2; i++)
		{
			dice[i] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;

			dice[i].transform.position = opponentDiceOrigin+new Vector3(-0.1f + 0.2f*i, Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

			
			dice[i].transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
			dice[i].rigidbody.angularVelocity = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
			dice[i].rigidbody.AddForce(direction*150f);
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
				for(int i = 0; i < dice.Length; i++)
					Destroy(dice[i]);
				state = State.OPPONENTPRETHROW;

				Destroy(lightparent);
				lightparent = new GameObject("LightParent");
				informationsText.enabled = false;
			}

		}
	}


	void PlayerPostThrow()
	{
		if(timer <= 0.0f)
		{
			int nrOfSleepingDice = 0;
			for (int i = dice.Length/2; i < dice.Length; i++) {
				if(dice[i].rigidbody.velocity == Vector3.zero)
					nrOfSleepingDice++;
			}
			if (nrOfSleepingDice >= dice.Length/2)
			{
				int playerPoints = 0;
				for(int i = dice.Length/2; i < dice.Length; i++)
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
					playerCounter.GetComponent<GUIText> ().text = winsPlayer.ToString();
					for(int i = dice.Length/2; i < dice.Length; i++)
					{
						Light light = new GameObject("Light").AddComponent<Light>();
						light.gameObject.transform.parent = lightparent.transform;
						light.type =  LightType.Spot;
						light.transform.position = dice[i].transform.position + new Vector3(0,.15f, 0);
						light.transform.LookAt(dice[i].transform.position);
						light.color = Color.green;
						light.intensity = 0.2f;
					}

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
					opponentCounter.GetComponent<GUIText> ().text = winsOpponent.ToString();

					for(int i = 0; i < dice.Length/2; i++)
					{
						Light light = new GameObject("Light").AddComponent<Light>();
						light.gameObject.transform.parent = lightparent.transform;
						light.type =  LightType.Spot;
						light.transform.position = dice[i].transform.position + new Vector3(0,.15f, 0);
						light.transform.LookAt(dice[i].transform.position);
						light.color = Color.red;
						light.intensity = 0.2f;
					}
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
			}
		}
		else {

			if(!Input.GetMouseButton(0))
			{
				holdingDownMouseButton = false;
				endHold = Input.mousePosition;
				if(endHold.y < startHold.y)
					endHold.y = startHold.y;
				state = State.PLAYERPOSTTHROW;
				
				informationsText.enabled = false;
				startHold = dicecamera.camera.ScreenPointToRay(startHold).origin;
				endHold = dicecamera.camera.ScreenPointToRay(endHold).origin;
				endHold.y = startHold.y;
				Vector3 direction = (endHold  - startHold);
				timer = totalTime;

				for(int i = dice.Length/2; i < dice.Length; i++)
				{
					dice[i] = GameObject.Instantiate(Resources.Load ("Die")) as GameObject;
					dice[i].transform.position = playerDiceOrigin+new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, -0.1f), Random.Range(-0.1f, 0.1f));

					dice[i].transform.Rotate(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					dice[i].rigidbody.angularVelocity = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
					if(direction.magnitude < 0.3f)
						direction = direction.normalized*0.3f;
					dice[i].rigidbody.AddForce(direction*200f);
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
