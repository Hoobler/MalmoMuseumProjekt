using UnityEngine;
using System.Collections;

public class LevelLoadTest : MonoBehaviour {
	public string levelToLoad;

	void OnMouseDown(){
        if (this.gameObject.name == "NyttSpel")
            PlayerPrefs.DeleteAll();
		Application.LoadLevel(levelToLoad);
		Debug.Log ("PRESSED");
	}
}
