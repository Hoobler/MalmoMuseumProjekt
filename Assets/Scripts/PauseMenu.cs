using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	bool paused;

	void Start () {
		paused = false;
		Screen.showCursor = false;
		Debug.Log("PauseMenu Start");
	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape) && paused == false){
			Screen.showCursor = true;
			paused = true;
		}

		if(Input.GetKeyDown(KeyCode.Escape) && paused == true){
			Screen.showCursor = false;
			paused = false;
		}
	}
}
