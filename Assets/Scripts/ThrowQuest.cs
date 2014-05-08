using UnityEngine;
using System.Collections;

public class ThrowQuest : QuestBase {

	bool questActive = false;
	bool questStart = false;

	int applesToThrow;
	int applesInBasket;
	int nrOfApplesForSuccess;
	Transform player;
	Transform basket;
	Transform startPoint;
	GameObject apple;
	float timeBetweenThrows;
	float totalTimeBetweenThrows;
	int nrOfActiveApples;
	GameObject chargeBar;
	GameObject chargeBarAmount;
	GameObject chargeBarBack;

	float charge;
	float chargeRate;
	bool charging;
	bool appleIsInTheAir;

	public Texture appleImage;
	GameObject reminder;

	public void OnGUI()
	{
		if(questActive)
			for(int i = 0; i < applesToThrow; i++)
			{
			GUI.DrawTexture(new Rect(Screen.width*0.01f*i, Screen.width*0.01f, Screen.width*0.032f, Screen.width*0.032f), appleImage);
			}
	}

	public void Update()
	{
		if (questActive) {
			if(timeBetweenThrows > 0)
				timeBetweenThrows -= Time.deltaTime;
			else
				appleIsInTheAir = false;
			player.position = new Vector3 (startPoint.position.x, player.position.y, startPoint.position.z);

			player.LookAt (new Vector3(basket.position.x, player.position.y, basket.position.z));
			(chargeBarAmount.GetComponent<GUITexture> ()).pixelInset = new Rect (Screen.width / 20f, Screen.height * (0.275f + 0.4f * charge), Screen.width *0.05f, Screen.height * 0.03f);
			if (questStart) {
				if (applesToThrow > 0 && !appleIsInTheAir) {
					if(Input.GetMouseButtonDown(0) && !charging) {
						charging = true;
					}
					else if(charging)
					{
						if(Input.GetMouseButton(0)) {
							if(chargeRate >= 0)
								chargeRate = 0.002f + 0.025f* charge;
							else
								chargeRate = -0.002f - 0.025f*charge;
							charge += chargeRate*Time.deltaTime*60;
							if (charge > 1.0f) {
								charge = 1.0f;
								chargeRate = -chargeRate;
							} else if (charge < 0.0f) {
								charge = 0.0f;
								chargeRate = -chargeRate;
							}
						}
						else
						{
							charging = false;
							Toss ();
						}
					}
				}
			}

			if (!questStart && Input.GetMouseButtonDown (0))
				questStart = true;
		}
	}

	public void FinishCheck()
	{
		nrOfActiveApples--;
		if (nrOfActiveApples <= 0 && applesToThrow <= 0)
			TriggerFinish (false);
	}


	public void Toss()
	{
		nrOfActiveApples++;
		timeBetweenThrows = totalTimeBetweenThrows;
		appleIsInTheAir = true;
		apple = Instantiate(Resources.Load("Apple")) as GameObject;
		apple.transform.position = player.position;
		apple.transform.position += player.forward * 0.5f;
		apple.rigidbody.AddForce ((basket.position + new Vector3(0f,7f,0f) - player.position).normalized*(400f+400f*charge));
		applesToThrow--;
		charge = 0.0f;
	}

	public void AppleTrigger(bool hit)
	{
		appleIsInTheAir = false;
		if (hit)
		{
			Instantiate(Resources.Load ("FadeCorrect"));
			applesInBasket++;
			if(applesInBasket >= nrOfApplesForSuccess)
				TriggerFinish(true);
		}
	}
	public void Init()
	{
		nrOfActiveApples = 0;
		questStart = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		basket = GameObject.Find("QuestBasketTrigger").transform;
		startPoint = GameObject.Find ("AppleQuestStartPoint").transform;
		chargeBar = (GameObject)Instantiate(Resources.Load ("ChargeBar"));
		chargeBarAmount = GameObject.Find ("ChargeAmount");
		chargeBarBack = GameObject.Find ("ChargeBackground");
		(chargeBarBack.GetComponent<GUITexture> ()).pixelInset = new Rect (Screen.width / 20f, Screen.height / 4f, Screen.width / 20f, Screen.height / 2f);
		(chargeBarAmount.GetComponent<GUITexture> ()).pixelInset = new Rect (Screen.width / 20f, Screen.height * 0.3f, Screen.width *0.05f, Screen.height * 0.05f);
		applesToThrow = 6;
		nrOfApplesForSuccess = 6;
		applesInBasket = 0;
		charge = 0f;
		chargeRate = 0.009f;
		totalTimeBetweenThrows = 1.0f;

	}

	void Start()
	{

		reminder = (GameObject)Instantiate (Resources.Load ("ReminderText"));
		reminder.transform.parent = gameObject.transform.parent;
	}

	public override void TriggerStart ()
	{
		base.TriggerStart ();
		Init ();
		questActive = true;
        ((GUITexture)(GameObject.Find("Karta")).GetComponentInChildren(typeof(GUITexture))).enabled = false;
		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript> ()).ChangeText ("Tryck på skärmen för att starta kastet. Sluta tryck för att kasta äpplet. Försök träffa så många som möjligt!");

	}

	public override void TriggerFinish (bool success)
	{
		base.TriggerFinish (success);
		string finishInfo;
		if (success)
						finishInfo = "Du klarade det!";
				else
						finishInfo = "Tyvärr, du träffade bara " + applesInBasket + " äpplen.";
		GameObject endDiag = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		GUIText endText = (GUIText)endDiag.GetComponentInChildren (typeof(GUIText));
		endText.text = finishInfo;
		reminder.SetActive (false);
		questActive = false;
		if(chargeBar != null)
			Destroy (chargeBar);
        ((GUITexture)(GameObject.Find("Karta")).GetComponentInChildren(typeof(GUITexture))).enabled = true;
	}
}
