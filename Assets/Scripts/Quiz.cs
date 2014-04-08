using UnityEngine;
using System.Collections;

[System.Serializable]
public class Question{
	public string question;
	public string[] answers;
	public int correctAnswer;
	//public Texture backgroundTexture;
	//[System.NonSerialized]
	//public GUITexture background;
	//[System.NonSerialized]
	//public GUIText text;
}

public class Quiz : MonoBehaviour {

	public Question[] questions;

	GUIText questionText;
	GameObject[] buttons;
	int currentQuestion = 0;
	GameObject quizParent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TriggerStart()
	{
		quizParent = new GameObject ("QuizParent");
		buttons = new GameObject[4];
		for (int i = 0; i < 4; i++) {
			buttons [i] = (GameObject)Instantiate (Resources.Load ("Button"));
			buttons[i].transform.parent = quizParent.transform;
			SetButtonText(buttons[i], questions[0].answers[i]);
			buttons[i].transform.position = new Vector3(0.25f*i%2, 0.25f*(i/2), 0); 
		}

		SetValues ();
	}

	void TriggerFinish()
	{
		Destroy (quizParent);
	}

	void NextQuestion()
	{
		currentQuestion++;
		SetValues ();
	}

	void SetValues()
	{
		for (int i = 0; i < questions[currentQuestion].answers.Length; i++) { 
			SetButtonText(buttons [i], questions [currentQuestion].answers [i]);
			SetButtonEnabled(buttons[i], true);
		}
		for (int i = questions[currentQuestion].answers.Length; i < 4; i++)
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
		GUIText guiText = (GUIText)button.GetComponentInChildren (typeof(GUIText));
		guiText.text = text;
	}
}
