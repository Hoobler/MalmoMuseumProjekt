using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;


public class ReminderTextScript : MonoBehaviour {

	bool closedDown = false;
	public Rect backgroundBounds;
	GUIText mainText;
	GUITexture background;
	bool isMoving = false;
	Vector2 openPosition;
	Vector2 closedPosition;
	float totalTimeBetweenTransitions;
	float currentTransitionTimer;

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

//	void CloseDown()
//	{
//		closedDown = true;
//		background.pixelInset = backgroundBoundsSmall;
//		gameObject.GetComponent<GUIText> ().enabled = false;
//	}
//
//	void BringUp()
//	{
//		closedDown = false;
//		background.pixelInset = backgroundBounds;
//		gameObject.GetComponent<GUIText> ().enabled = true;
//	}

	// Use this for initialization
	void Start () {

		openPosition = new Vector2(Screen.width * 0.8f, Screen.height * 0.4f);
		closedPosition = new Vector2(Screen.width * 0.95f, Screen.height * 0.4f);
		totalTimeBetweenTransitions = 1f;
		currentTransitionTimer = 0f;
		background = gameObject.GetComponent<GUITexture> ();
		backgroundBounds = new Rect (Screen.width * 0.8f, Screen.height * 0.4f, Screen.width * 0.15f, Screen.height * 0.4f);
		background.pixelInset = backgroundBounds;

		mainText = gameObject.GetComponent<GUIText> ();
		mainText.color = Color.black;
		mainText.text = "Default Text 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15";
		mainText.pixelOffset = new Vector2 (backgroundBounds.x + backgroundBounds.width * 0.1f, backgroundBounds.yMax - backgroundBounds.height * 0.1f);
		mainText.fontSize = (int)(12 * Screen.width / 800f);



		gameObject.SetActive (false);
	}

	void Update() {
		if(isMoving)
		{
			if(currentTransitionTimer >= totalTimeBetweenTransitions)
			{
				if(!closedDown){
					gameObject.guiTexture.pixelInset = new Rect(closedPosition.x, gameObject.guiTexture.pixelInset.y, gameObject.guiTexture.pixelInset.width, gameObject.guiTexture.pixelInset.height);
					gameObject.guiText.pixelOffset = new Vector2(closedPosition.x + backgroundBounds.width * 0.1f, gameObject.guiText.pixelOffset.y);
				}
				else
				{
					gameObject.guiTexture.pixelInset = new Rect(openPosition.x, gameObject.guiTexture.pixelInset.y, gameObject.guiTexture.pixelInset.width, gameObject.guiTexture.pixelInset.height);
					gameObject.guiText.pixelOffset = new Vector2(openPosition.x + backgroundBounds.width * 0.1f, gameObject.guiText.pixelOffset.y);
					gameObject.guiText.enabled = true;
				}
				closedDown = !closedDown;
				currentTransitionTimer = 0;
				isMoving = false;


			}
			else
			{
				currentTransitionTimer+= Time.deltaTime;
				if(!closedDown){

					float xChangeRatio = openPosition.x+(closedPosition.x-openPosition.x)*(currentTransitionTimer/totalTimeBetweenTransitions);

					gameObject.guiTexture.pixelInset = new Rect(xChangeRatio,
				                                            gameObject.guiTexture.pixelInset.y,
				                                            gameObject.guiTexture.pixelInset.width,
				                                            gameObject.guiTexture.pixelInset.height);
					gameObject.guiText.pixelOffset = new Vector2(xChangeRatio + backgroundBounds.width * 0.1f,
				                                             gameObject.guiText.pixelOffset.y);
				}
				else
				{
					float xChangeRatio = closedPosition.x-(closedPosition.x-openPosition.x)*(currentTransitionTimer/totalTimeBetweenTransitions);
					gameObject.guiTexture.pixelInset = new Rect(xChangeRatio,
					                                            gameObject.guiTexture.pixelInset.y,
					                                            gameObject.guiTexture.pixelInset.width,
					                                            gameObject.guiTexture.pixelInset.height);
					gameObject.guiText.pixelOffset = new Vector2(xChangeRatio + backgroundBounds.width * 0.1f,
					                                             gameObject.guiText.pixelOffset.y);
				}
			}
		}
	}


	void OnMouseDown() {
		if(!isMoving){
			isMoving = true;
			gameObject.guiText.enabled = false;
			//if(!closedDown)
			//{
			//	CloseDown();
			//}
			//else
			//	BringUp();
		}
	}

}
