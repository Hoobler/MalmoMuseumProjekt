using UnityEngine;
using System.Collections;

public class playerCounterInitScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GUIText> ().fontSize = (int)(32 * Screen.width / 800f);
		gameObject.GetComponent<GUITexture> ().pixelInset = new Rect (Screen.width * 0.01f, Screen.height * 0.75f, Screen.height * 0.1f, Screen.height * 0.1f);
		gameObject.GetComponent<GUIText> ().pixelOffset = new Vector2 (gameObject.GetComponent<GUITexture> ().GetScreenRect ().xMax, gameObject.GetComponent<GUITexture> ().GetScreenRect ().center.y);
		gameObject.GetComponent<GUIText> ().anchor = TextAnchor.MiddleLeft;
		gameObject.SetActive (false);
	}
}
