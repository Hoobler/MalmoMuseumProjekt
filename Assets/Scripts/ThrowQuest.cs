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
	Color startColor;

	public Texture appleImage;
	public Texture kastKnapp;
	GameObject reminder;
	GameObject endNotification;
	GameObject throwButton;

	public void OnGUI()
	{
		if(questActive)
			for(int i = 0; i < applesToThrow; i++)
			{
				GUI.DrawTexture(new Rect(Screen.width*0.02f*i + Screen.width*0.035f, Screen.height*0.8f, Screen.width*0.06f, Screen.width*0.06f), appleImage);
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
					if(Input.GetMouseButtonDown(0) && throwButton.guiTexture.GetScreenRect().Contains(Input.mousePosition) && !charging) {
						charging = true;
						throwButton.guiTexture.color = new Color(0.35f, 0.35f, 0.35f);
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
							throwButton.guiTexture.color = startColor;
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
		GameObject apple = Instantiate(Resources.Load("Apple")) as GameObject;
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
		throwButton = new GameObject ("ThrowButton");
		throwButton.transform.parent = chargeBar.transform.parent;
		throwButton.transform.position = Vector3.zero;
		throwButton.transform.localScale = Vector3.zero;
		throwButton.AddComponent<GUITexture> ();
		throwButton.guiTexture.texture = kastKnapp;
		throwButton.guiTexture.pixelInset = new Rect (Screen.width*0.45f, Screen.height*0.05f, Screen.width*0.1f, Screen.width*0.1f);
		startColor = throwButton.guiTexture.color;
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
		endNotification = (GameObject)Instantiate (Resources.Load ("QuestEndDialogue"));
		endNotification.transform.parent = gameObject.transform.parent;

	}

	public override void TriggerStart ()
	{
		base.TriggerStart ();
		Init ();
		questActive = true;
        ((GUITexture)(GameObject.Find("Karta")).GetComponentInChildren(typeof(GUITexture))).enabled = false;
		reminder.SetActive (true);
		((ReminderTextScript)reminder.GetComponent<ReminderTextScript> ()).ChangeText ("Ladda ditt kast genom att hålla ned kastknappen. Mätaren till vänster visar hur hårt du kommer att kasta. Släpp kastknappen för att kasta.");

	}

	public override void TriggerFinish (bool success)
	{
		base.TriggerFinish (success);

		//PREFS
		if (success) {
			if (PlayerPrefs.GetInt ("LTquest") == 0)
				PlayerPrefs.SetInt ("LTquest", 2);
			else if (PlayerPrefs.GetInt ("LTquest") == 1)
				PlayerPrefs.SetInt ("LTquest", 3);
		}
		//---

		string finishInfo = "Du klarade det! Och vet du vad, Malmö kallades faktiskt för Ellenbogen innan det hette Malmö.";
		if (success)
			finishInfo = "Du klarade det! Och vet du vad, Malmö kallades faktiskt för Ellenbogen innan det hette Malmö.";
		else if (!success && applesInBasket < 3)
			finishInfo = "Tyvärr, du träffade bara " + applesInBasket + " äpplen. Du får försöka kasta bättre ifall du vill få någon information från mig.";
		else if (!success && applesInBasket < 6)
			finishInfo = "Tyvärr, du träffade bara " + applesInBasket + " äpplen. Och nu till informationen som jag skulle ge dig, ifall du inte har märkt det så är det mest Hantverk och Mat som handlas här.";


		endNotification.GetComponent<endNotificationScript> ().Activate (finishInfo);

		reminder.SetActive (false);
		questActive = false;
		if(chargeBar != null)
			Destroy (chargeBar);
        ((GUITexture)(GameObject.Find("Karta")).GetComponentInChildren(typeof(GUITexture))).enabled = true;
	}
}
