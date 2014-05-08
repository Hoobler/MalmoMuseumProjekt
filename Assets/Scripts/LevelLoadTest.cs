using UnityEngine;
using System.Collections;

public class LevelLoadTest : MonoBehaviour {
	public string levelToLoad;

	void OnMouseDown(){
		Application.LoadLevel(levelToLoad);
		Debug.Log ("PRESSED");
	}
}
