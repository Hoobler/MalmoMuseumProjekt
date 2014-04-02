using UnityEngine;
using System.Collections;

public class CanonBallScript : MonoBehaviour {

	private const float LIFE_TIME = 5f;
	private float timer = 0;
	private bool hit = false;

	CanonQuest quest;

	// Use this for initialization
	void Start () {
		quest = GameObject.Find ("QuestGiverCanon").GetComponent (typeof(CanonQuest)) as CanonQuest;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= LIFE_TIME) {
			quest.CanonBallTrigger(hit);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider){
		if (!hit && collider.gameObject.name == "Hull") {
			hit = true;
			Debug.Log ("CANONBALL COLLISION");
			quest.CanonBallTrigger(hit);
			Destroy(gameObject);
		}
	}
}
