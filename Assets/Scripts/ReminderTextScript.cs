using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;


public class ReminderTextScript : MonoBehaviour {

	bool closedDown = false;
	Rect backgroundBounds;
	GUIText mainText;
	GUITexture background;


	public void ChangeText(string newText)
	{
		gameObject.SetActive (true);
		mainText.text = newText;
		FormatMainText ();
	}

	void FormatMainText(){
		string[] text = gameObject.GetComponent<GUIText> ().text.Split(' ');
		gameObject.GetComponent<GUIText> ().text = "";
		
		for (int i = 0; i < text.GetLength(0); i++) {
			
			gameObject.GetComponent<GUIText> ().text += text[i] + " ";
			if(gameObject.GetComponent<GUIText> ().GetScreenRect().width > backgroundBounds.width * 0.8f)
			{
				gameObject.GetComponent<GUIText> ().text = gameObject.GetComponent<GUIText> ().text.Substring(0, gameObject.GetComponent<GUIText> ().text.Length - text[i].Length - 1);
				gameObject.GetComponent<GUIText> ().text += "\n" + text[i] + " ";
			}
		}
	}

	void CloseDown()
	{
		closedDown = true;
		background.pixelInset = new Rect (Screen.width * 0.93f, Screen.height * 0.4f, Screen.width * 0.02f, Screen.height * 0.02f);
		backgroundBounds = background.GetScreenRect ();
		gameObject.GetComponent<GUIText> ().enabled = false;
	}

	void BringUp()
	{
		closedDown = false;
		background.pixelInset = new Rect (Screen.width * 0.8f, Screen.height * 0.4f, Screen.width * 0.15f, Screen.height * 0.4f);
		backgroundBounds = background.GetScreenRect ();
		gameObject.GetComponent<GUIText> ().enabled = true;
	}

	// Use this for initialization
	void Start () {
		background = gameObject.GetComponent<GUITexture> ();
		background.pixelInset = new Rect (Screen.width * 0.8f, Screen.height * 0.4f, Screen.width * 0.15f, Screen.height * 0.4f);
		backgroundBounds = background.GetScreenRect ();
		mainText = gameObject.GetComponent<GUIText> ();
		mainText.color = Color.black;
		mainText.text = "Default Text 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15";
		mainText.pixelOffset = new Vector2 (backgroundBounds.x + backgroundBounds.width * 0.1f, backgroundBounds.yMax - backgroundBounds.height * 0.1f);
		mainText.fontSize = (int)(12 * Screen.width / 800f);

		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		if(!closedDown)
		{
			CloseDown();
		}
		else
			BringUp();
	}

}
