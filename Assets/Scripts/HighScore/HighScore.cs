using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	public int RowPadding;

	private bool _showScore;
	private string [,] _scoreArray;

	// Use this for initialization
	void Start () {
		_scoreArray = new string[5 ,2]{{"Sven", "10"},{"Ben", "20"},{"Dave", "5"},{"Erik", "1"},{"Adam", "200"}};
		_showScore = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Rita ut poängen etc.
	void OnGUI(){
		if(_showScore){
			for(int i = 0; i < 5 ; i++){
				GUI.Label (new Rect (70, 50 + (i * 15), 400, 20),"Name: " + _scoreArray[i,0] + " Score: " + _scoreArray[i,1]);
			}
		}
	}

	void AddScore(string name, int score){
		//stuff so much work!?!?!?!?
	}
	//Om vi faktiskt får server plats från malmö museum. 
	//Kan det sparas på servern och laddas in när man uppdaterar.
	void LoadScore(){

	}
}
