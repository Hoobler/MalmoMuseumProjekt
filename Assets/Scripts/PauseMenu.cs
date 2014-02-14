using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GUIText guiText;
	bool paused;

	void Start () {
		paused = false;
		Debug.Log("PauseMenu Start");
		guiText.text = "Null";
	}

	void Update () {
		if(paused == false){
			Screen.lockCursor = true;
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			Screen.showCursor = true;
			Screen.lockCursor = false;
			paused = true;
			guiText.text = "Paused";
		}
	}
}
