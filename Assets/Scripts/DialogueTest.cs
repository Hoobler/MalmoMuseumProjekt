using UnityEngine;
using System.Collections;

[System.Serializable]
public class Knapp{
	public bool quitOnPress = false;
	public string textOnButton = "Knapptext";
	public string shownInfoWhenPressed = "Du tryckte på en knapp";
	public Texture backgroundTexture;
	public bool isQuestTrigger;
	public string nameOfQuest;
	[System.NonSerialized]
	public GUITexture background;
	[System.NonSerialized]
	public GUIText text;
}

public class DialogueTest: MonoBehaviour {

	public string introText = "Här ska det stå introduktionstext";
	public Font font;

	public Texture backgroundTexture;

	public float speechDistance = 10.0f;
	public float abortConversationDistance = 20.0f;
	private QuestManager questManager;

	GUIText mainGUIText;

	GUITexture background;

	GameObject dialogueObject;

	private Transform playerTransform;

	private bool conversationActive = false;

	public Knapp[] buttons; 

	void Start(){

	}

	void FormatMainText(){
		string[] text = mainGUIText.text.Split(' ');
		mainGUIText.text = "";

		for (int i = 0; i < text.GetLength(0); i++) {

			mainGUIText.text += text[i] + " ";
			if(mainGUIText.GetScreenRect().width > background.GetScreenRect().width * 0.8f)
			{
				mainGUIText.text = mainGUIText.text.Substring(0, mainGUIText.text.Length - text[i].Length - 1);
				mainGUIText.text += "\n" + text[i] + " ";
			}
		}
	}

	void CreateNewButtons()
	{
		float offsetY = 0.045f;

		for (int i = 0; i < buttons.Length; i++) {
				GameObject buttonParent = new GameObject ("Button");
				buttonParent.transform.parent = dialogueObject.transform;

				GameObject backObject = new GameObject ("Background");
				backObject.transform.parent = buttonParent.transform;
				buttons [i].background = (GUITexture)backObject.AddComponent (typeof(GUITexture));
				buttons [i].background.texture = buttons [i].backgroundTexture;
			
			buttons [i].background.transform.localScale = new Vector3 (0.20f, 0.07f, 0);
				buttons [i].background.transform.position = new Vector3 (0.0f, 0f, 0.1f);
		
				GameObject textObject = new GameObject ("Text");
				textObject.transform.parent = buttonParent.transform;
				buttons [i].text = (GUIText)textObject.AddComponent (typeof(GUIText));
				buttons [i].text.text = buttons [i].textOnButton;
				buttons [i].text.transform.position = new Vector3 (0f, 0f, 0.11f);
				buttons [i].text.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
				buttons [i].text.color = new Color (0, 0, 0);
				buttons[i].text.alignment = TextAlignment.Center;
				buttons[i].text.anchor = TextAnchor.MiddleCenter;
				if (font)
						buttons [i].text.font = font;
				buttonParent.transform.localScale = new Vector3(1,1,1);
				buttonParent.transform.position = new Vector3(0, -0.23f+offsetY*(buttons.Length-i - 1), 0.1f);


		}
	}

	void Init(){

		dialogueObject = new GameObject ("Dialogue");
		questManager = GameObject.Find ("Quest_Handler").GetComponent (typeof(QuestManager)) as QuestManager;

		GameObject backgroundHolder = new GameObject ("Background");
		backgroundHolder.transform.parent = dialogueObject.transform;

		GameObject backObject = new GameObject ("Background");
		backObject.transform.parent = backgroundHolder.transform;
		background = (GUITexture)backObject.AddComponent (typeof(GUITexture));
		background.texture = backgroundTexture;
		background.transform.position =  new Vector3 (0f, 0f, 0);
		background.transform.localScale = new Vector3 (0.3f, 0.6f, 0);

		GameObject mainGUITextObject = new GameObject ("MainText");
		mainGUITextObject.transform.parent = backgroundHolder.transform;
		mainGUIText = (GUIText)mainGUITextObject.AddComponent (typeof(GUIText));
		mainGUIText.text = introText;
		mainGUIText.transform.position =  new Vector3 (-0.12f, 0.25f, 0.10f);
		mainGUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
		mainGUIText.color = new Color (0, 0, 0);
		mainGUIText.lineSpacing = 1f;
		if(font)
			mainGUIText.font = font;

		CreateNewButtons ();
		dialogueObject.transform.position = new Vector3 (0.2f, 0.5f, 0);

		FormatMainText ();

	}


	void KillConversation(){
		Destroy (dialogueObject);
		conversationActive = false;
		}

	void Update(){
		if (conversationActive) {
			if (Input.GetMouseButtonDown (0)) {


					for(int i = 0; i < buttons.Length; i++)
					{
						if (buttons[i].background.GetScreenRect ().Contains (Input.mousePosition)) {
					
							if (questManager && buttons[i].isQuestTrigger)
								questManager.ActivateQuest(buttons[i].nameOfQuest);
					
							if(buttons[i].quitOnPress)
							{
								KillConversation();
								break;
							}
					
							mainGUIText.text = buttons[i].shownInfoWhenPressed;
							FormatMainText();
							break;
						}
					}

			}
			if(playerTransform && Vector3.Distance(playerTransform.position, this.transform.position) > abortConversationDistance)
				KillConversation();
		}
	}
	void OnMouseDown(){
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		if(Vector3.Distance(playerTransform.position, this.transform.position) < speechDistance)
			if (!conversationActive) {
				Init ();
				conversationActive = true;
			}		
	}

}
