using UnityEngine;
using System.Collections;

public class SpionVictimScript : MonoBehaviour {

	SpionQuest sQuest;

	// Use this for initialization
	void Start () {
		sQuest = (SpionQuest)GameObject.Find ("QuestGiverSpion").GetComponent (typeof(SpionQuest));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		sQuest.SpyTrigger (gameObject);
	}
}
