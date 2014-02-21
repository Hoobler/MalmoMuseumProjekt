using UnityEngine;
using System.Collections;

[System.Serializable]
public class Knapp{
	public bool enabled = true;
	public bool quitOnPress = false;
	public string textOnButton = "Knapptext";
	public string shownInfoWhenPressed = "Du tryckte på en knapp";
	public Texture backgroundTexture;
	public bool isQuestTrigger;
	public string nameOfQuest;
}

public class DialogueTest: MonoBehaviour {

	public string introText = "Här ska det stå introduktionstext";
	public Font font;
	public Knapp button1;
	public Knapp button2;
	public Knapp button3;
	public Knapp button4;

	public Texture backgroundTexture;

	public float speechDistance = 10.0f;
	public float abortConversationDistance = 20.0f;
	private QuestManager questManager;

	GUIText mainGUIText;
	GUIText button1GUIText;
	GUIText button2GUIText;
	GUIText button3GUIText;
	GUIText button4GUIText;

	GUITexture background;
	GUITexture button1Back;
	GUITexture button2Back;
	GUITexture button3Back;
	GUITexture button4Back;

	GameObject dialogueObject;

	GameObject backObject;

	GameObject mainGUITextObject;
	GameObject button1BackObject;
	GameObject button1TextObject;

	GameObject button2BackObject;
	GameObject button2TextObject;

	GameObject button3BackObject;
	GameObject button3TextObject;

	GameObject button4BackObject;
	GameObject button4TextObject;


	private Transform playerTransform;

	private bool conversationActive = false;

	void Start(){

	}

	void FormatMainText(){

	}

	void Init(){

		dialogueObject = new GameObject ("Dialogue");
		questManager = GameObject.Find ("Quest_Handler").GetComponent (typeof(QuestManager)) as QuestManager;
		backObject = new GameObject ("DialogueBackground");
		backObject.transform.parent = dialogueObject.transform;
		background = (GUITexture)backObject.AddComponent (typeof(GUITexture));
		background.texture = backgroundTexture;
		background.transform.position =  new Vector3 (0.2f, 0.5f, 0);
		background.transform.localScale = new Vector3 (0.3f, 0.6f, 0);

		mainGUITextObject = new GameObject ("DialogueMainText");
		mainGUITextObject.transform.parent = dialogueObject.transform;
		mainGUIText = (GUIText)mainGUITextObject.AddComponent (typeof(GUIText));
		mainGUIText.text = introText;
		mainGUIText.transform.position =  new Vector3 (0.08f, 0.75f, 0.10f);
		mainGUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
		mainGUIText.color = new Color (0, 0, 0);
		if(font)
			mainGUIText.font = font;

		if (button1.enabled) {
			button1BackObject = new GameObject ("DialogueButton1Background");
			button1BackObject.transform.parent = dialogueObject.transform;
			button1Back = (GUITexture)button1BackObject.AddComponent (typeof(GUITexture));
			button1Back.texture = button1.backgroundTexture;
			button1Back.transform.position = new Vector3 (0.12f, 0.4f, 0.1f);
			button1Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);

			button1TextObject = new GameObject ("DialogueButton1Text");
			button1TextObject.transform.parent = dialogueObject.transform;
			button1GUIText = (GUIText)button1TextObject.AddComponent (typeof(GUIText));
			button1GUIText.text = button1.textOnButton;
			button1GUIText.transform.position = new Vector3 (0.08f, 0.415f, 0.11f);
			button1GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button1GUIText.color = new Color (0, 0, 0);
			if(font)
				button1GUIText.font = font;
		}

		if (button2.enabled) {
			button2BackObject = new GameObject ("DialogueButton2Background");
			button2BackObject.transform.parent = dialogueObject.transform;
			button2Back = (GUITexture)button2BackObject.AddComponent (typeof(GUITexture));
			button2Back.texture = button2.backgroundTexture;
			button2Back.transform.position = new Vector3 (0.28f, 0.4f, 0.1f);
			button2Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			
			button2TextObject = new GameObject ("DialogueButton2Text");
			button2TextObject.transform.parent = dialogueObject.transform;
			button2GUIText = (GUIText)button2TextObject.AddComponent (typeof(GUIText));
			button2GUIText.text = button2.textOnButton;
			button2GUIText.transform.position = new Vector3 (0.24f, 0.415f, 0.11f);
			button2GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button2GUIText.color = new Color (0, 0, 0);
			if(font)
				button2GUIText.font = font;
		}

		if (button3.enabled) {
			button3BackObject = new GameObject ("DialogueButton3Background");
			button3BackObject.transform.parent = dialogueObject.transform;
			button3Back = (GUITexture)button3BackObject.AddComponent (typeof(GUITexture));
			button3Back.texture = button3.backgroundTexture;
			button3Back.transform.position = new Vector3 (0.12f, 0.3f, 0.1f);
			button3Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);

			button3TextObject = new GameObject ("DialogueButton3Text");
			button3TextObject.transform.parent = dialogueObject.transform;
			button3GUIText = (GUIText)button3TextObject.AddComponent (typeof(GUIText));
			button3GUIText.text = button3.textOnButton;
			button3GUIText.transform.position = new Vector3 (0.08f, 0.315f, 0.11f);
			button3GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button3GUIText.color = new Color (0, 0, 0);
			if(font)
				button3GUIText.font = font;
		}

		if (button4.enabled) {
			button4BackObject = new GameObject ("DialogueButton4Background");
			button4BackObject.transform.parent = dialogueObject.transform;
			button4Back = (GUITexture)button4BackObject.AddComponent (typeof(GUITexture));
			button4Back.texture = button4.backgroundTexture;
			button4Back.transform.position = new Vector3 (0.28f, 0.3f, 0.1f);
			button4Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);

			button4TextObject = new GameObject ("DialogueButton4Text");
			button4TextObject.transform.parent = dialogueObject.transform;
			button4GUIText = (GUIText)button4TextObject.AddComponent (typeof(GUIText));
			button4GUIText.text = button4.textOnButton;
			button4GUIText.transform.position = new Vector3 (0.24f, 0.315f, 0.11f);
			button4GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button4GUIText.color = new Color (0, 0, 0);
			if(font)
				button3GUIText.font = font;
		}
		//SendMessage ("QuestTrigger", 5);


	}

//	void QuestTrigger(int number){
//		buttonQuitGUIText.text = "" + number;
//		Destroy (buttonQuitBackObject);
//		}

	void KillConversation(){
		Destroy (dialogueObject);
		conversationActive = false;
		}

	void Update(){
		if (conversationActive) {
			if (Input.GetMouseButtonDown (0)) {
				if (button1Back != null)
					if (button1Back.GetScreenRect ().Contains (Input.mousePosition)) {
						mainGUIText.text = button1.shownInfoWhenPressed;
						
						if (questManager && button1.isQuestTrigger)
							questManager.ActivateQuest(button1.nameOfQuest);

						if(button1.quitOnPress)
							KillConversation();
					}
								
				if (button2Back != null)
					if (button2Back.GetScreenRect ().Contains (Input.mousePosition)) {
						mainGUIText.text = button2.shownInfoWhenPressed;
						if (questManager && button2.isQuestTrigger)
							questManager.ActivateQuest(button2.nameOfQuest);
						if(button2.quitOnPress)
							KillConversation();
					}
								
				if (button3Back != null)
					if (button3Back.GetScreenRect ().Contains (Input.mousePosition)) {
						mainGUIText.text = button3.shownInfoWhenPressed;
						if (questManager && button3.isQuestTrigger)
							questManager.ActivateQuest(button3.nameOfQuest);
						if(button3.quitOnPress)
							KillConversation();
					}
								
				if (button4Back != null)
					if (button4Back.GetScreenRect ().Contains (Input.mousePosition)) {
						mainGUIText.text = button4.shownInfoWhenPressed;
						if (questManager && button4.isQuestTrigger)
							questManager.ActivateQuest(button4.nameOfQuest);
						if(button4.quitOnPress)
							KillConversation();
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
