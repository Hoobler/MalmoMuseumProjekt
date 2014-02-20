using UnityEngine;
using System.Collections;


public class DialogueTest: MonoBehaviour {

	public string introText = "Här ska det stå introduktions-text";
	public bool Knapp1 = true;
	public string button1Text = "Knapp1";
	public string button1Info = "Trycker du på knapp1 kommer detta fram";
	public bool Knapp2 = true;
	public string button2Text = "Knapp2";
	public string button2Info = "Trycker du på knapp2 kommer detta fram";
	public bool Knapp3 = true;
	public string button3Text = "Knapp3";
	public string button3Info = "Trycker du på knapp3 kommer detta fram";
	public string buttonQuitText = "Avsluta";

	public Texture backgroundTexture;
	public Texture button1Texture;
	public Texture button2Texture;
	public Texture button3Texture;
	public Texture buttonQuitTexture;

	public float speechDistance = 10.0f;
	public float abortConversationDistance = 20.0f;
	GUIText mainGUIText;
	GUIText button1GUIText;
	GUIText button2GUIText;
	GUIText button3GUIText;
	GUIText buttonQuitGUIText;

	GUITexture background;
	GUITexture button1Back;
	GUITexture button2Back;
	GUITexture button3Back;
	GUITexture buttonQuitBack;

	GameObject dialogueObject;

	GameObject backObject;
	GameObject mainGUITextObject;
	GameObject button1BackObject;
	GameObject button1TextObject;
	GameObject button2BackObject;
	GameObject button2TextObject;
	GameObject button3BackObject;
	GameObject button3TextObject;
	GameObject buttonQuitBackObject;
	GameObject buttonQuitTextObject;

	public string QuestFunction;

	private TriggerActivation tact;
	private Transform temp;

	private bool conversationActive = false;

	void Start(){

	}

	void Init(){
		
		dialogueObject = new GameObject ("Dialogue");

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

		if (Knapp1) {
			button1BackObject = new GameObject ("DialogueButton1Background");
			button1BackObject.transform.parent = dialogueObject.transform;
			button1Back = (GUITexture)button1BackObject.AddComponent (typeof(GUITexture));
			button1Back.texture = button1Texture;
			button1Back.transform.position = new Vector3 (0.12f, 0.4f, 0.1f);
			button1Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			
			button1TextObject = new GameObject ("DialogueButton1Text");
			button1TextObject.transform.parent = dialogueObject.transform;
			button1GUIText = (GUIText)button1TextObject.AddComponent (typeof(GUIText));
			button1GUIText.text = button1Text;
			button1GUIText.transform.position = new Vector3 (0.08f, 0.415f, 0.11f);
			button1GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button1GUIText.color = new Color (0, 0, 0);
		}

		if (Knapp2) {
			button2BackObject = new GameObject ("DialogueButton2Background");
			button2BackObject.transform.parent = dialogueObject.transform;
			button2Back = (GUITexture)button2BackObject.AddComponent (typeof(GUITexture));
			button2Back.texture = button2Texture;
			button2Back.transform.position = new Vector3 (0.28f, 0.4f, 0.1f);
			button2Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			
			button2TextObject = new GameObject ("DialogueButton2Text");
			button2TextObject.transform.parent = dialogueObject.transform;
			button2GUIText = (GUIText)button2TextObject.AddComponent (typeof(GUIText));
			button2GUIText.text = button2Text;
			button2GUIText.transform.position = new Vector3 (0.24f, 0.415f, 0.11f);
			button2GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button2GUIText.color = new Color (0, 0, 0);
		}

		if (Knapp3) {
			button3BackObject = new GameObject ("DialogueButton3Background");
			button3BackObject.transform.parent = dialogueObject.transform;
			button3Back = (GUITexture)button3BackObject.AddComponent (typeof(GUITexture));
			button3Back.texture = button3Texture;
			button3Back.transform.position = new Vector3 (0.12f, 0.3f, 0.1f);
			button3Back.transform.localScale = new Vector3 (0.1f, 0.05f, 0);

			button3TextObject = new GameObject ("DialogueButton3Text");
			button3TextObject.transform.parent = dialogueObject.transform;
			button3GUIText = (GUIText)button3TextObject.AddComponent (typeof(GUIText));
			button3GUIText.text = button3Text;
			button3GUIText.transform.position = new Vector3 (0.08f, 0.315f, 0.11f);
			button3GUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
			button3GUIText.color = new Color (0, 0, 0);
		}

		buttonQuitBackObject = new GameObject ("DialogueButtonQuitBackground");
		buttonQuitBackObject.transform.parent = dialogueObject.transform;
		buttonQuitBack = (GUITexture)buttonQuitBackObject.AddComponent (typeof(GUITexture));
		buttonQuitBack.texture = buttonQuitTexture;
		buttonQuitBack.transform.position =  new Vector3 (0.28f, 0.3f, 0.1f);
		buttonQuitBack.transform.localScale = new Vector3 (0.1f, 0.05f, 0);

		buttonQuitTextObject = new GameObject ("DialogueButtonQuitText");
		buttonQuitTextObject.transform.parent = dialogueObject.transform;
		buttonQuitGUIText = (GUIText)buttonQuitTextObject.AddComponent (typeof(GUIText));
		buttonQuitGUIText.text = buttonQuitText;
		buttonQuitGUIText.transform.position =  new Vector3 (0.24f, 0.315f, 0.11f);
		buttonQuitGUIText.transform.localScale = new Vector3 (0.1f, 0.05f, 0);
		buttonQuitGUIText.color = new Color (0, 0, 0);

		tact = GameObject.FindGameObjectWithTag ("Player").GetComponent ("TriggerActivation") as TriggerActivation;

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
										mainGUIText.text = button1Info;
										if (tact)
												tact.ShowPickupQuest ();
								}
								if (button2Back != null)
								if (button2Back.GetScreenRect ().Contains (Input.mousePosition)) {
										mainGUIText.text = button2Info;
								}
								if (button3Back != null)
								if (button3Back.GetScreenRect ().Contains (Input.mousePosition)) {
										mainGUIText.text = button3Info;
								}
								if (buttonQuitBack != null)
								if (buttonQuitBack.GetScreenRect ().Contains (Input.mousePosition)) {
									KillConversation();

								}
						}
			if(temp && Vector3.Distance(temp.position, this.transform.position) > abortConversationDistance)
				KillConversation();
				}
	}

	void OnMouseDown(){
		temp = GameObject.FindGameObjectWithTag ("Player").transform;
		if(Vector3.Distance(temp.position, this.transform.position) < speechDistance)
			if (!conversationActive) {
				Init ();
				conversationActive = true;
			}		
	}
}
