using UnityEngine;
using System.Collections;

public class SpionQuest : QuestBase {

	public Texture background;
	public Texture[] spionTexturer;
	public string[] namn;
	bool questActive;
	GameObject dialogueWindow;

	Rect[] choiceBounds;

	// Use this for initialization
	void Start () {
		questActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(questActive){
			if(Input.GetMouseButtonUp(0))
			{
				for(int i = 0; i < 3; i++)
				{
					if(choiceBounds[i].Contains(Input.mousePosition))
					{
						TriggerFinish(true);
					}
				}
			}
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
		backTexture.pixelInset = new Rect (Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f);

		for(int i = 0; i < 3; i++)
		{
			GameObject val = new GameObject("Valruta"+i);
			val.transform.localScale = Vector3.zero;
			val.transform.parent = dialogueWindow.transform;
			GUITexture guiTexture = val.AddComponent<GUITexture>();
			guiTexture.texture = spionTexturer[i];
			choiceBounds[i] = new Rect(backTexture.GetScreenRect().x + backTexture.GetScreenRect().width*0.05f + backTexture.GetScreenRect ().width*0.3f*i,
			                           backTexture.GetScreenRect().y + backTexture.GetScreenRect ().height*0.15f,
			                           backTexture.GetScreenRect ().width*0.29f,
			                           backTexture.GetScreenRect().height*0.8f);
			guiTexture.pixelInset = choiceBounds[i];
			GUIText guiText = val.AddComponent<GUIText>();
			guiText.text = namn[i];
			guiText.fontSize = (int)(12 * Screen.width / 800f);
			guiText.alignment = TextAlignment.Center;
			guiText.anchor = TextAnchor.MiddleCenter;
			guiText.pixelOffset = new Vector2(choiceBounds[i].center.x, choiceBounds[i].y - guiText.fontSize);
			guiText.color = Color.black;

		}
	}

	public override void TriggerFinish (bool success)
	{
		base.TriggerFinish (success);
		Destroy (dialogueWindow);
	}
}
