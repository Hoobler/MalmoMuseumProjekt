using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateQuest(string name)
	{
		Debug.Log ("Quest '" + name + "' activated");
	}

}
