using UnityEngine;
using System.Collections;

public class ThrowQuest : QuestBase {

	bool questActive = false;
	bool questStart = false;

	int applesToThrow;
	int applesInBasket;
	Transform player;
	Transform basket;
	Transform startPoint;
	GameObject apple;
	float timeBetweenThrows;
	float totalTimeBetweenThrows;
	int nrOfActiveApples;

	Transform chargeBar;

	float charge; //pedja kom på detta
	float chargeRate;
	bool charging;
	bool appleIsInTheAir;

	public void Update()
	{
		if (questActive) {
			if(timeBetweenThrows > 0)
				timeBetweenThrows -= Time.deltaTime;
			else
				appleIsInTheAir = false;
			player.position = new Vector3 (startPoint.position.x, player.position.y, startPoint.position.z);
			//Debug.Log ("" + startPoint.position.x + "=" + player.position.x + ", " + startPoint.position.z + "=" + player.position.z);
			player.LookAt (new Vector3(basket.position.x, player.position.y, basket.position.z));
			chargeBar.position = new Vector3 (chargeBar.position.x, 0.3f + 0.4f * charge, chargeBar.position.z);
			if (!questStart && Input.GetMouseButtonDown (0))
				questStart = true;

			if (questStart) {
				if (applesToThrow > 0 && !appleIsInTheAir) {
					if (Input.GetMouseButton (0)) {
						if (charging) {
							if(chargeRate >= 0)
								chargeRate = 0.002f + 0.025f* charge;
							else
								chargeRate = -0.002f - 0.025f*charge;
							charge += chargeRate;
							Debug.Log ("" + charge);
							if (charge > 1.0f) {
									charge = 1.0f;
									chargeRate = -chargeRate;
							} else if (charge < 0.0f) {
									charge = 0.0f;
									chargeRate = -chargeRate;
							}
						} else
							charging = true;
					} else if (charging) {
						charging = false;
						Toss ();
					}
				}
			}
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
			Debug.Log("COLLIDE");
			applesInBasket++;
			if(applesInBasket >= 6)
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
		Instantiate(Resources.Load ("ChargeBar"));
		chargeBar = GameObject.Find ("ChargeAmount").transform;

		applesToThrow = 10;
		applesInBasket = 0;
		charge = 0f;
		chargeRate = 0.009f;
		totalTimeBetweenThrows = 1.0f;

	}

	public override void TriggerStart ()
	{
		base.TriggerStart ();
		Init ();
		questActive = true;

	}

	public void TriggerFinish (bool success)
	{
		if (success)
						Debug.Log ("YAY");
				else
						Debug.Log ("NAY");
		questActive = false;
		if(chargeBar.root != null)
			Destroy (chargeBar.root.gameObject);
	}
}
