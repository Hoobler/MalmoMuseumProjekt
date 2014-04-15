using UnityEngine;
using System.Collections;

[System.Serializable]
public class Question{
	public string question;
	public string[] answers;
	public int correctAnswer;
}

public class Quiz : MonoBehaviour {

	public Question[] questions;
	public bool randomizeQuestions;
	ArrayList listOfAnswers;
	ArrayList listOfQuestions;
	Rect backgroundBounds;
	GUIText questionText;
	GameObject[] buttons;
	int currentQuestion;
	GameObject quizParent;
	bool active = false;
	int points;
	bool isChoosingAnswer;

	int selectedAnswer;

	// Use this for initialization
	void Start () {
	
	}


	void EndScreen()
	{
		if (points == 0)
			questionText.text = "Du klarade inte en enda fråga. =(";
		else if (points == questions.Length)
			questionText.text = "Grattis, du klarade alla frågor!";
		else
			questionText.text = "Grattis! Du klarade " + points + " av " + questions.Length + " frågor!";
		FormatMainText ();
		for (int i = 0; i < buttons.Length; i++) {
			SetButtonEnabled(buttons[i], false);
		}
	}


	void Update() {
		if (active) {
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
									points++;
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

	public void TriggerStart()
	{
		quizParent = new GameObject ("QuizParent");
		GameObject background = (GameObject)Instantiate (Resources.Load ("Quizmain"));
		background.transform.parent = quizParent.transform;
		backgroundBounds = ((GUITexture)background.GetComponentInChildren (typeof(GUITexture))).GetScreenRect ();
		questionText = (GUIText)background.GetComponentInChildren (typeof(GUIText));
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
			buttons[i].transform.position = new Vector3(0.25f + 0.25f*(i%2), 0.25f+0.1f*(i/2), 0); 
		}

		SetValues ();
	}

	public void TriggerFinish()
	{
		active = false;
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
		if (!active) {
			TriggerStart ();
			active = true;
		}
	}
}
