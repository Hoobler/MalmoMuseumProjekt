using UnityEngine;
using System.Collections;

public class ThrowQuest : QuestBase {

	bool questActive = false;

	int applesToThrow;
	int applesInBasket;
	Transform player;
	Transform basket;
	float charge; //pedja kom på detta
	float chargeRate;
	bool charging;

	public void Update()
	{
		if (questActive) {
			if(Input.GetMouseButtonDown(0))
			{
				charging = true;
				charge += chargeRate;
				if(charge > 1.0f)
				{
					charge = 1.0f;
					chargeRate = -chargeRate;
				}
				else if(charge < 0.0f)
				{
					charge = 0.0f;
					chargeRate = -chargeRate;
				}
			}
			else if(charging)
			{
				charging = false;
				Toss ();
			}
		}
	}


	public void Toss()
	{
		applesToThrow--;
		if (applesToThrow <= 0)
			TriggerFinish ();

	}
	public void Init()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		basket = GameObject.FindGameObjectWithTag ("AppleBasket").transform;
		player.LookAt (basket);
		applesToThrow = 10;
		applesInBasket = 0;
		charge = 0f;
		chargeRate = 0.005f;

	}

	public override void TriggerStart ()
	{
		base.TriggerStart ();
		Init ();
		questActive = true;

	}

	public override void TriggerFinish ()
	{
		base.TriggerFinish ();
	}
}
