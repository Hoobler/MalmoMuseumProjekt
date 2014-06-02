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
	GameObject endDiag;
	Rect[] choiceBounds;
	bool mouseButtonWasPreviouslyDown = false;
	bool canceledSpionQuest = false;
	bool hasChosenSpy = false;
	int chosen = 0;
	int antalPratatMed = 0;
	int antalAttPrataMed = 3;

	// Use this for initialization
	void Start () {
		antalPratatMed = 0;
		chosen = 0;
		hasChosenSpy = false;
		questActive = false;
		endDiag = (GameObject)Instantiate(Resources.Load ("QuestEndDialogue"));
		endDiag.transform.parent = GameObject.Find ("Spion").transform;

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
						chosen = i;
						TriggerFinish(true);
					}
				}
				if(cancelKnapp.guiTexture.GetScreenRect().Contains(Input.mousePosition))
				{
					canceledSpionQuest = true;
					TriggerFinish(false);
				}
			}
			else
				mouseButtonWasPreviouslyDown = false;
		}
	}

	public override void TriggerStart ()
	{
		if (antalPratatMed < antalAttPrataMed) {
			TriggerFinish (false);
			return;
		}
		if (hasChosenSpy) {
			TriggerFinish (true);
			return;
		}

		canceledSpionQuest = false;
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
		if (success) {
			if(!hasChosenSpy){
				endDiag.GetComponent<endNotificationScript> ().Activate ("Tack för din hjälp, vi har det i åtanke.\n\n"+namn[chosen]+" kommer nog skickas till Värnhem för att hängas. ROLIGT VA");
				hasChosenSpy = true;
				if (PlayerPrefs.GetInt ("Gquest") == 0)
					PlayerPrefs.SetInt ("Gquest", 1);
				else if (PlayerPrefs.GetInt ("Gquest") == 2)
					PlayerPrefs.SetInt ("Gquest", 3);
			}
			else
				endDiag.GetComponent<endNotificationScript> ().Activate ("Tack för tanken, men du har redan pekat ut någon. " + namn[chosen] + " kommer att skickas till Värnhem.");

		} else if(!success){
			if(canceledSpionQuest)
				endDiag.GetComponent<endNotificationScript> ().Activate ("Ok, kom tillbaka när du känner dig mer säker.");
			else
				endDiag.GetComponent<endNotificationScript> ().Activate ("Jag tror du måste prata med fler folk innan du pekar ut någon.");			
		}
		base.TriggerFinish (success);
		Destroy (dialogueWindow);
	}

	public void PratatMedFolk(){
		antalPratatMed++;
	}
}
