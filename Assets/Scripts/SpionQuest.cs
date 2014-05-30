using UnityEngine;
using System.Collections;

public class SpionQuest : QuestBase {

	public Texture background;
	public Texture buttonTexture;
	public Texture[] spionTexturer;
	public string[] namn;
	bool questActive;
	GameObject dialogueWindow;
	GameObject cancelKnapp;
	Rect[] choiceBounds;
	bool mouseButtonWasPreviouslyDown = false;

	// Use this for initialization
	void Start () {
		questActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(questActive){
			if(Input.GetMouseButtonUp(0) && !mouseButtonWasPreviouslyDown)
			{
				mouseButtonWasPreviouslyDown = true;
				for(int i = 0; i < 3; i++)
				{
					if(choiceBounds[i].Contains(Input.mousePosition))
					{
						TriggerFinish(true);
					}
				}
				if(cancelKnapp.guiTexture.GetScreenRect().Contains(Input.mousePosition))
				{
					TriggerFinish(false);
				}
			}
			else
				mouseButtonWasPreviouslyDown = false;
		}
	}

	public override void TriggerStart ()
	{
		choiceBounds = new Rect[3];
		questActive = true;
		dialogueWindow = new GameObject ("SpionFonster");
		dialogueWindow.transform.localScale = Vector3.zero;
		dialogueWindow.transform.position = new Vector3 (0, 0, -1);
		GUITexture backTexture = dialogueWindow.AddComponent<GUITexture> ();
		backTexture.texture = background;
		backTexture.pixelInset = new Rect (Screen.width * 0.25f, Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * 0.5f);
		mouseButtonWasPreviouslyDown = true;
		for(int i = 0; i < 3; i++)
		{
			GameObject val = new GameObject("Valruta"+i);
			val.transform.localScale = Vector3.zero;
			val.transform.parent = dialogueWindow.transform;
			GUITexture guiTexture = val.AddComponent<GUITexture>();
			guiTexture.texture = spionTexturer[i];
			choiceBounds[i] = new Rect(backTexture.GetScreenRect().x + backTexture.GetScreenRect().width*0.05f + backTexture.GetScreenRect ().width*0.3f*i,
			                           backTexture.GetScreenRect().yMax - backTexture.GetScreenRect ().height*0.05f - backTexture.GetScreenRect ().width*0.29f,
			                           backTexture.GetScreenRect ().width*0.29f,
			                           backTexture.GetScreenRect ().width*0.29f);
			guiTexture.pixelInset = choiceBounds[i];
			GUIText guiText = val.AddComponent<GUIText>();
			guiText.text = namn[i];
			guiText.fontSize = (int)(12 * Screen.width / 800f);
			guiText.alignment = TextAlignment.Center;
			guiText.anchor = TextAnchor.MiddleCenter;
			guiText.pixelOffset = new Vector2(choiceBounds[i].center.x, choiceBounds[i].y - guiText.fontSize);
			guiText.color = Color.black;
		}

		cancelKnapp = new GameObject ("CancelKnapp");
		cancelKnapp.transform.localScale = Vector3.zero;
		cancelKnapp.transform.parent = dialogueWindow.transform;
		cancelKnapp.AddComponent<GUITexture> ();
		cancelKnapp.guiTexture.texture = buttonTexture;
		cancelKnapp.transform.position = new Vector3 (0, 0, 2);

		cancelKnapp.AddComponent<GUIText> ();
		cancelKnapp.guiText.text = "Jag vet inte vem som är spion";
		cancelKnapp.guiText.fontSize = (int)(12 * Screen.width / 800f);
		cancelKnapp.guiText.color = Color.black;
		cancelKnapp.guiText.alignment = TextAlignment.Center;
		cancelKnapp.guiText.anchor = TextAnchor.MiddleCenter;
		cancelKnapp.guiText.pixelOffset = new Vector2 (backTexture.GetScreenRect().center.x,
		                                               backTexture.GetScreenRect().y + backTexture.GetScreenRect().height*0.1f);
		cancelKnapp.guiTexture.pixelInset = new Rect (backTexture.GetScreenRect().x+backTexture.GetScreenRect().width*0.2f,
		                                              backTexture.GetScreenRect().y,
		                                              backTexture.GetScreenRect().width*0.6f,
		                                              backTexture.GetScreenRect().height*0.2f);

	}

	public override void TriggerFinish (bool success)
	{
		questActive = false;
		base.TriggerFinish (success);
		Destroy (dialogueWindow);
	}
}
