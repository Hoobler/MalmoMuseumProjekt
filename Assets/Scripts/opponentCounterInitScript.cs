using UnityEngine;
using System.Collections;

public class opponentCounterInitScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GUIText> ().fontSize = (int)(32 * Screen.width / 800f);
		gameObject.GetComponent<GUITexture> ().pixelInset = new Rect (Screen.width * 0.55f, Screen.height * 0.85f, Screen.height * 0.1f, Screen.height * 0.1f);
		gameObject.GetComponent<GUIText> ().pixelOffset = new Vector2 (gameObject.GetComponent<GUITexture> ().GetScreenRect ().xMax, gameObject.GetComponent<GUITexture> ().GetScreenRect ().yMax);
		gameObject.SetActive (false);
	}
}
