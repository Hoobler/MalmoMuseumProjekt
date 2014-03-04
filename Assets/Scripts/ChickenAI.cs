using UnityEngine;
using System.Collections;

public class ChickenAI : MonoBehaviour {

	public enum ChickenState{
		PANICKED,
		IDLE,
		EATING,
		CREEPY
	}

	float speed = 1f;
	float panicDistance = 2;
	float notPanickedDistance = 5;
	public ChickenState state = ChickenState.CREEPY;
	bool playerFound = false;
	float hungerlevel =0f;
	Vector3 targetPos;
	// Update is called once per frame
	void Update () {

		Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
		Vector3 relativePos = player.position - transform.position;
		relativePos.y = 0;
		if (state == ChickenState.CREEPY) {
			animation.CrossFade ("Idle_2");

			Quaternion rotation = Quaternion.LookRotation (relativePos);
			transform.rotation = rotation;
			float deltaAngle = Quaternion.Angle (transform.rotation, player.rotation);

			if (deltaAngle < 50) {
				Vector3 newPos = player.position - relativePos.normalized * 3f;
				newPos.y = 0f;
				transform.position = newPos;
			}
		}
		else if(state == ChickenState.IDLE){
			animation.CrossFade("Walk");


			transform.position += transform.forward*speed*Time.deltaTime;

			hungerlevel += Time.deltaTime;
			if(hungerlevel >= 1)
			{
				hungerlevel = 5;
				state = ChickenState.EATING;
			}
			if(relativePos.magnitude < panicDistance)
				state = ChickenState.PANICKED;
		}
		else if(state == ChickenState.PANICKED){
			animation.CrossFade("Walk");

			Quaternion rotation = Quaternion.LookRotation(-relativePos);
			transform.rotation = rotation;
			transform.position += transform.forward * speed * Time.deltaTime;
			if(relativePos.magnitude > notPanickedDistance)
				state = ChickenState.IDLE;
		}
		else if (state == ChickenState.EATING) {
			animation.CrossFade("Eat");
			hungerlevel -= Time.deltaTime;
			if(hungerlevel <= 0.0f)
			{
				state = ChickenState.IDLE;
				if(Random.Range(0,7) < 4)
					targetPos = transform.right*5f;
				else
					targetPos = -transform.right*5f;
				transform.rotation = Quaternion.LookRotation (targetPos);
			}
			if(relativePos.magnitude < panicDistance)
				state = ChickenState.PANICKED;
		}
	}
}
