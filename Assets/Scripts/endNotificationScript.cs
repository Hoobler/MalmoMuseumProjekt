using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class endNotificationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GUITexture> ().pixelInset = new Rect (Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f);
		gameObject.GetComponent<GUIText> ().pixelOffset = new Vector2 (gameObject.GetComponent<GUITexture> ().GetScreenRect ().x + gameObject.GetComponent<GUITexture> ().GetScreenRect ().width * 0.1f,
		                                                              gameObject.GetComponent<GUITexture> ().GetScreenRect ().yMax - gameObject.GetComponent<GUITexture> ().GetScreenRect ().height * 0.1f);
		gameObject.GetComponent<GUIText> ().fontSize = (int)(16 * Screen.width / 800f);
	}

	public void Activate(string newText)
	{
		gameObject.GetComponent<GUIText> ().text = newText;
		FormatMainText ();
		gameObject.SetActive (true);
	}

	void FormatMainText(){
		string[] text = gameObject.GetComponent<GUIText> ().text.Split(' ');
		gameObject.GetComponent<GUIText> ().text = "";
		
		for (int i = 0; i < text.GetLength(0); i++) {
			
			gameObject.GetComponent<GUIText> ().text += text[i] + " ";
			if(gameObject.GetComponent<GUIText> ().GetScreenRect().width > gameObject.GetComponent<GUITexture>().GetScreenRect().width * 0.8f)
			{
				gameObject.GetComponent<GUIText> ().text = gameObject.GetComponent<GUIText> ().text.Substring(0, gameObject.GetComponent<GUIText> ().text.Length - text[i].Length - 1);
				gameObject.GetComponent<GUIText> ().text += "\n" + text[i] + " ";
			}
		}
	}

	void OnMouseDown()
	{
		Destroy (gameObject);
	}
}
