using UnityEngine;
using System.Collections;

[System.Serializable]
public class Question{
	public string question;
	public string[] answers;
	public int correctAnswer;
}
public class Quiz : MonoBehaviour {

	public bool randomizeQuestions;
	public Question[] questions;

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
		FormatMainText ();
		for (int i = 0; i < buttons.Length; i++) {
			SetButtonEnabled(buttons[i], false);
		}
	}


	void Update() {
		if (quizActive) {
			if(Input.GetMouseButtonDown(0))
			{
				if(isChoosingAnswer)
				{
					if (listOfQuestions.Count != 0)
					{
						for(int i = 0; i < questions [(int)listOfQuestions[currentQuestion]].answers.Length; i++)
						{
							if(((GUITexture)buttons[i].GetComponentInChildren(typeof(GUITexture))).GetScreenRect().Contains(Input.mousePosition))
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

								((GUITexture)buttons[selectedAnswer].GetComponentInChildren(typeof(GUITexture))).color = Color.red;
								((GUITexture)buttons[questions [(int)listOfQuestions[currentQuestion]].correctAnswer].GetComponentInChildren(typeof(GUITexture))).color = Color.green;
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

	public void ActivatePreQuiz()
	{

	}

	public void ActivateQuiz()
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
			((GUIText)buttons[i].GetComponentInChildren (typeof(GUIText))).fontSize = (int)(12 * Screen.width / 800f);
			if(i%2 == 0)
				buttons[i].transform.position = new Vector3(0.5f - 0.15f, 0.25f+0.1f*(i/2), 0); 
			if(i%2 == 1)
				buttons[i].transform.position = new Vector3(0.5f + 0.15f, 0.25f+0.1f*(i/2), 0);
		}
		
		SetValues ();
	}

	public void TriggerStart()
	{
		quizHasActuallyStarted = false;
		ActivateQuiz ();
	}

	public void TriggerFinish()
	{
		quizActive = false;
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
			((GUITexture)buttons[i].GetComponentInChildren(typeof(GUITexture))).color = Color.black;
		}
		for (int i = questions [(int)listOfQuestions[currentQuestion]].answers.Length; i < 4; i++)
			SetButtonEnabled(buttons[i], false);
	}

	void SetButtonEnabled(GameObject button, bool desiredState)
	{
		GUIText text = (GUIText)button.GetComponentInChildren (typeof(GUIText));
		GUITexture background = (GUITexture)button.GetComponentInChildren (typeof(GUITexture));

		text.enabled = desiredState;
		background.enabled = desiredState;
	}

	void SetButtonText(GameObject button, string text)
	{
		((GUIText)button.GetComponentInChildren (typeof(GUIText))).text = text;
	}

	void OnMouseDown()
	{
		if (!quizActive && !GameObject.Find ("Quest_Handler").GetComponent<QuestManager> ().IsQuestInProgress()) {
			TriggerStart ();
			quizActive = true;
		}
	}
}
