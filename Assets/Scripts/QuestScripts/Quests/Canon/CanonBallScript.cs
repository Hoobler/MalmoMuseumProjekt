using UnityEngine;
using System.Collections;

public class CanonBallScript : MonoBehaviour {

	private const float LIFE_TIME = 5f;
	private float timer = 0;
	private bool hit = false;

	private ParticleSystem water;

	CanonQuest quest;

	// Use this for initialization
	void Start () {
		quest = GameObject.Find ("QuestGiverCanon").GetComponent (typeof(CanonQuest)) as CanonQuest;
		water = GameObject.Find ("WaterEffect").GetComponent ("ParticleSystem") as ParticleSystem;

		if (water) {
			water.Stop();
			water.Clear();
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= LIFE_TIME) {
			quest.CanonBallTrigger(hit);
			Destroy(gameObject);
		}

		if (transform.position.y < 3.5) {
			water.Stop();
			water.Clear();
			water.transform.position = transform.position;
			water.Play();	
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider){
		if (!hit && collider.gameObject.name == "Hull") {
			hit = true;
			Instantiate(Resources.Load("FadeCorrect"));
			Debug.Log ("CANONBALL COLLISION : " + collider.gameObject.name);
			quest.CanonBallTrigger(hit);
			Destroy(gameObject);
		}
	}
}
