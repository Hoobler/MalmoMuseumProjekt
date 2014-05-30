using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

[System.Serializable]
public class Question{
	public string question;
	public string[] answers;
	public int correctAnswer;
}
public class Quiz : MonoBehaviour {

	public Texture texture;
	public string introText = "QUIZ YEAH!";
	public string outroText = "";
	public bool randomizeQuestions;
	public Question[] questions;
	public int speechDistance = 20;

	ArrayList listOfAnswers;
	ArrayList listOfQuestions;
	Rect backgroundBounds;
	GUIText questionText;
	GameObject[] buttons;
	int currentQuestion;
	GameObject quizParent;
	bool quizActive = false;
	int points;
	bool isChoosingAnswer;

	bool previousActionWasMouseButtonDown = false;

	int selectedAnswer;

	bool quizHasActuallyStarted;

	// Use this for initialization
	void Start () {
		quizHasActuallyStarted = false;
	}


	void EndScreen()
	{
		if (points == 0)
			questionText.text = "Du klarade inte en enda fråga. =(";
		else if (points == questions.Length) {
			questionText.text = "Grattis, du klarade alla frågor!";
			if(Application.loadedLevel == 0)
				PlayerPrefs.SetInt("LTquiz", 1);
			else if(Application.loadedLevel == 2)
				PlayerPrefs.SetInt("Squiz", 1);
			else if(Application.loadedLevel == 3)
				PlayerPrefs.SetInt("Gquiz", 1);
		}
			
		else
			questionText.text = "Grattis! Du klarade " + points + " av " + questions.Length + " frågor!";

		questionText.text = questionText.text + "\n"+ outroText;
		FormatMainText ();
		for (int i = 0; i < buttons.Length; i++) {
			SetButtonEnabled(buttons[i], false);
		}
	}


	void Update() {
		if (quizActive) {
			if(Input.GetMouseButtonDown(0) && !previousActionWasMouseButtonDown)
			{
				previousActionWasMouseButtonDown = true;
				if(quizHasActuallyStarted){
					if(isChoosingAnswer)
					{
						if (listOfQuestions.Count != 0)
						{
							for(int i = 0; i < questions [(int)listOfQuestions[currentQuestion]].answers.Length; i++)
							{
								if(buttons[i].guiTexture.GetScreenRect().Contains(Input.mousePosition))
								{
									selectedAnswer = i;
									if(selectedAnswer == questions [(int)listOfQuestions[currentQuestion]].correctAnswer)
									{
										points++;
										Instantiate(Resources.Load ("FadeCorrect"));
									}
									else
										Instantiate(Resources.Load ("FadeWrong"));
									isChoosingAnswer = false;

									buttons[selectedAnswer].guiTexture.color = Color.red;
									buttons[questions [(int)listOfQuestions[currentQuestion]].correctAnswer].guiTexture.color = Color.green;
									break;
								}
							}
						}
						else
						{
							TriggerFinish();
						}
					}
					else
					{
						isChoosingAnswer = true;
						NextQuestion();
					}
				}
				else
				{
					if(buttons[0].guiTexture.GetScreenRect().Contains(Input.mousePosition))
					{
						quizHasActuallyStarted = true;
						Destroy(quizParent);
						ActivateQuiz();
					}
					else if(buttons[1].guiTexture.GetScreenRect().Contains(Input.mousePosition))
					{
						TriggerFinish();
					}
				}
			}
			else
				previousActionWasMouseButtonDown = false;
		}
	}

	void FormatMainText(){
		string[] text = questionText.text.Split(' ');
		questionText.text = "";
		
		for (int i = 0; i < text.GetLength(0); i++) {
			
			questionText.text += text[i] + " ";
			if(questionText.GetScreenRect().width > backgroundBounds.width * 0.8f)
			{
				questionText.text = questionText.text.Substring(0, questionText.text.Length - text[i].Length - 1);
				questionText.text += "\n" + text[i] + " ";
			}
		}
	}

	void ActivatePreQuiz()
	{
		quizParent = new GameObject ("QuizParent");
		GameObject background = new GameObject ("QuizPredialogue");
		background.transform.parent = quizParent.transform;
		background.AddComponent<GUIText> ();
		background.guiText.fontSize = (int)(14 * Screen.width / 800f);
		background.guiText.text = introText;
		//background.guiText.text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(background.guiText.text));

		background.AddComponent<GUITexture> ();
		background.guiTexture.texture = texture;
		background.transform.position = new Vector3(0,0,-1);
		background.transform.localScale = Vector3.zero;
		background.guiTexture.pixelInset = new Rect (Screen.width * 0.3f, Screen.height *0.3f, Screen.width * 0.4f, Screen.height * 0.4f);
		background.guiText.pixelOffset = new Vector2 (background.guiTexture.GetScreenRect ().x + background.guiTexture.GetScreenRect ().width * 0.125f, background.guiTexture.GetScreenRect ().yMax - background.guiTexture.GetScreenRect ().height * 0.2f);
		background.guiText.color = Color.black;

		buttons = new GameObject[2];
		buttons[0] = (GameObject)Instantiate (Resources.Load ("Button"));
		buttons [0].transform.parent = quizParent.transform;
		buttons[0].guiText.text = "JA";
		buttons[0].guiText.fontSize = (int)(12 * Screen.width / 800f);
		buttons[0].guiText.pixelOffset = new Vector2 (Screen.width * 0.4f, Screen.height * 0.45f);
		buttons[0].guiTexture.pixelInset = new Rect (Screen.width * 0.35f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.1f);

		buttons[1] = (GameObject)Instantiate (Resources.Load ("Button"));
		buttons [1].transform.parent = quizParent.transform;
		buttons[1].guiText.text = "NEJ";
		buttons[1].guiText.fontSize = (int)(12 * Screen.width / 800f);
		buttons[1].guiText.pixelOffset = new Vector2 (Screen.width * 0.6f, Screen.height * 0.45f);
		buttons[1].guiTexture.pixelInset = new Rect (Screen.width * 0.55f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.1f);
	}

	void ActivateQuiz()
	{
		quizParent = new GameObject ("QuizParent");
		GameObject background = (GameObject)Instantiate (Resources.Load ("Quizmain"));
		background.transform.parent = quizParent.transform;
		
		//posititioning and shit. Mostly positioning.
		background.guiTexture.pixelInset = new Rect (Screen.width / 8, Screen.height / 8, 6 * Screen.width / 8, 6 * Screen.height / 8);
		backgroundBounds = background.guiTexture.GetScreenRect ();
		questionText = background.guiText;
		questionText.pixelOffset = new Vector2 (backgroundBounds.x + backgroundBounds.width *0.1f, backgroundBounds.y + backgroundBounds.height * 0.9f);
		questionText.fontSize = (int)(12 * Screen.width / 800f);
		
		isChoosingAnswer = true;
		if(randomizeQuestions)
			currentQuestion = Random.Range (0, questions.Length - 1);
		else
			currentQuestion = 0;
		points = 0;
		
		listOfQuestions = new ArrayList ();
		for (int i = 0; i < questions.Length; i++) {
			listOfQuestions.Add (i);
		}
		
		buttons = new GameObject[4];
		for (int i = 0; i < 4; i++) {
			buttons [i] = (GameObject)Instantiate (Resources.Load ("Button"));
			buttons[i].transform.parent = quizParent.transform;
			buttons[i].transform.position = new Vector3 (0,0,1);
			buttons[i].guiText.fontSize = (int)(12 * Screen.width / 800f);
			if(i%2 == 0)
			{
				//buttons[i].guiText.pixelOffset = new Vector2(Screen.width * (0.5f - 0.15f), Screen.height * (0.25f+0.1f*(i/2)));
				buttons[i].guiText.pixelOffset = new Vector2(backgroundBounds.x + backgroundBounds.width/4f, backgroundBounds.yMax -  backgroundBounds.height * 0.5f - (i/2) * backgroundBounds.height * 0.25f);
				//buttons[i].guiTexture.pixelInset = new Rect(Screen.width * (0.5f - 0.30f), Screen.height * (0.25f+0.1f*(i/2)), Screen.width * 0.3f, Screen.height*0.2f);
			}
			else if(i%2 == 1)
			{
				buttons[i].guiText.pixelOffset = new Vector2(backgroundBounds.x + backgroundBounds.width* 3f/4f, backgroundBounds.yMax - backgroundBounds.height * 0.5f - (i/2) * backgroundBounds.height * 0.25f);
				//buttons[i].guiTexture.pixelInset = new Rect(Screen.width * (0.5f), Screen.height * (0.25f+0.1f*(i/2)), Screen.width * 0.3f, Screen.height*0.2f);
			}
			buttons[i].guiTexture.pixelInset = new Rect(buttons[i].guiText.GetScreenRect().center.x - Screen.width*0.08f, buttons[i].guiText.GetScreenRect().center.y - Screen.height*0.08f, Screen.width*0.16f, Screen.height*0.16f);
		}
		
		SetValues ();
	}

	public void TriggerStart()
	{
		quizHasActuallyStarted = false;


		ActivatePreQuiz ();
	}

	public void TriggerFinish()
	{
		quizActive = false;
		quizHasActuallyStarted = false;
		Destroy (quizParent);
	}

	void NextQuestion()
	{
		listOfQuestions.RemoveAt (currentQuestion);
		if (listOfQuestions.Count == 0) {
			EndScreen ();
		} else {
			currentQuestion = Random.Range (0, listOfQuestions.Count - 1);
			SetValues ();
		}
	}

	void SetValues()
	{
		questionText.text = questions [(int)listOfQuestions[currentQuestion]].question;
		FormatMainText ();
		for (int i = 0; i < questions [(int)listOfQuestions[currentQuestion]].answers.Length; i++) { 
			SetButtonText(buttons [i], questions [(int)listOfQuestions[currentQuestion]].answers [i]);
			SetButtonEnabled(buttons[i], true);
			buttons[i].guiTexture.color = Color.black;
		}
		for (int i = questions [(int)listOfQuestions[currentQuestion]].answers.Length; i < 4; i++)
			SetButtonEnabled(buttons[i], false);
	}

	void SetButtonEnabled(GameObject button, bool desiredState)
	{
		button.guiText.enabled = desiredState;
		button.guiTexture.enabled = desiredState;
	}

	void SetButtonText(GameObject button, string text)
	{
		button.guiText.text = text;
	}

	void OnMouseDown()
	{

		if (!quizActive && !GameObject.Find ("Quest_Handler").GetComponent<QuestManager> ().IsQuestInProgress()) {
			if(Vector3.Distance(GameObject.FindGameObjectWithTag ("Player").transform.position, this.transform.position) < speechDistance){
				TriggerStart ();
				previousActionWasMouseButtonDown = true;
				quizActive = true;
			}
		}
	}
}
