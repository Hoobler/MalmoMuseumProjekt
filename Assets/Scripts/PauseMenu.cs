using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool _paused;

	void Start () {
		_paused = false;
		Debug.Log("PauseMenu Start");
	}
	
	void OnGUI(){
		if (_paused) {
			GUI.Label (new Rect (Screen.width /2 , 30, 30, 20), "Paused");
		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !_paused ){
			Debug.Log("Paused");
			Screen.showCursor = true;
			Screen.lockCursor = false;
			_paused = true;
		} 
		else if (Input.GetKeyDown(KeyCode.Escape) && _paused){
			Debug.Log("UnPaused");
			_paused = false;
			Screen.showCursor = false;
			Screen.lockCursor= true;
		}
	}
}
