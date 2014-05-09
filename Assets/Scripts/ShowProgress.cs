using UnityEngine;
using System.Collections;

public class ShowProgress : MonoBehaviour {

	private GUITexture goto_texture_gui;
	private GUITexture background_texture_gui;

	private int quest_complete;
	private int quiz_complete;

	public Texture goto_texture;
	public Texture background_texture;
	public Texture complete_texture;
	public Texture incomplete_texture;

	private GameObject background;
	private GameObject goto_button;

	void OnMouseDown(){
		Destroy (GameObject.Find ("GotoButton"));
		Destroy (GameObject.Find ("Background"));
		Destroy (GameObject.Find ("Quest1"));
		Destroy (GameObject.Find ("Quest2"));
		Destroy (GameObject.Find ("Quiz"));
		ShowSceneInfo ();
	}

	private void ShowSceneInfo(){
		goto_button = new GameObject ("GotoButton");
		background = new GameObject ("Background");

		background_texture_gui = (GUITexture)background.AddComponent (typeof(GUITexture));
		background_texture_gui.texture = background_texture;
		background_texture_gui.transform.localScale = Vector3.zero;
		background_texture_gui.transform.position = Vector3.zero;
		background_texture_gui.pixelInset = new Rect ((float)gameObject.guiTexture.GetScreenRect ().x, (float)gameObject.guiTexture.GetScreenRect ().y, 0.2f * Screen.width, 0.2f * Screen.width);  

		goto_texture_gui = (GUITexture)goto_button.AddComponent (typeof(GUITexture));
		goto_texture_gui.texture = goto_texture;
		goto_texture_gui.transform.localScale = Vector3.zero;
		goto_texture_gui.transform.position = new Vector3 (0, 0, 1);
		goto_texture_gui.pixelInset = new Rect ((float)background_texture_gui.GetScreenRect ().x,
		                                        (float)background_texture_gui.GetScreenRect ().y - background_texture_gui.pixelInset.height * 0.25f, 
		                                        background_texture_gui.pixelInset.width, 
		                                        (background_texture_gui.pixelInset.height * 0.8f) / 3.125f); 

		LevelLoadTest load = goto_button.AddComponent<LevelLoadTest> ();
		load.levelToLoad = gameObject.name;

		//quiz complete - 	0 - nej
		//					1 - ja

		//quest complete - 	0 - inga 
		//					1 - vete 
		//					2 - korg 
		//					3 - båda 
		if (gameObject.name == "LillaTorg") {
			quest_complete = PlayerPrefs.GetInt("LTquest");
			quiz_complete = PlayerPrefs.GetInt("LTquiz");
		}
		//quest complete - 	0 - inga 
		//					1 - musköt 
		//					2 - kanon 
		//					3 - båda 
		else if (gameObject.name == "Slottet") {
			quest_complete = PlayerPrefs.GetInt("Squest");
			quiz_complete = PlayerPrefs.GetInt("Squiz");
		}
		//quest complete - 	0 - inga 
		//					1 - tärning 
		//					2 - spion 
		//					3 - båda 
		else if (gameObject.name == "Grabroder") {
			quest_complete = PlayerPrefs.GetInt("Gquest");
			quiz_complete = PlayerPrefs.GetInt("Gquiz");
			
		}

		DrawCompletion ();

	}

	private void DrawCompletion(){

		GUITexture quest_1_gui;
		GUITexture quest_2_gui;
		GUITexture quiz_gui;
		GameObject quest_1 = new GameObject ("Quest1");
		GameObject quest_2 = new GameObject ("Quest2");
		GameObject quiz = new GameObject ("Quiz");
		
		quest_1_gui = quest_1.AddComponent<GUITexture>();
		quest_1_gui.transform.localScale = Vector3.zero;
		quest_1.transform.position = new Vector3(0, 0, 1);
		
		quest_1_gui.pixelInset = new Rect ((float)background.guiTexture.GetScreenRect ().x + background_texture_gui.pixelInset.width * 0.187f,
		                                   (float)gameObject.guiTexture.GetScreenRect ().y + background_texture_gui.pixelInset.width * 0.575f,
		                                   background_texture_gui.pixelInset.width * 0.042f,
		                                   background_texture_gui.pixelInset.width * 0.042f);  
		
		quest_2_gui = quest_2.AddComponent<GUITexture>();
		quest_2_gui.transform.localScale = Vector3.zero;
		quest_2.transform.position = new Vector3(0, 0, 1);
		
		quest_2_gui.pixelInset = new Rect ((float)background.guiTexture.GetScreenRect ().x + background_texture_gui.pixelInset.width * 0.187f,
		                                   (float)gameObject.guiTexture.GetScreenRect ().y + background_texture_gui.pixelInset.width * 0.473f,
		                                   background_texture_gui.pixelInset.width * 0.042f,
		                                   background_texture_gui.pixelInset.width * 0.042f);  

		quiz_gui = quiz.AddComponent<GUITexture> ();
		quiz_gui.transform.localScale = Vector3.zero;
		quiz_gui.transform.position = new Vector3 (0, 0, 1);

		quiz_gui.pixelInset = new Rect ((float)background.guiTexture.GetScreenRect ().x + background_texture_gui.pixelInset.width * 0.187f,
		                                   (float)gameObject.guiTexture.GetScreenRect ().y + background_texture_gui.pixelInset.width * 0.369f,
		                                   background_texture_gui.pixelInset.width * 0.042f,
		                                   background_texture_gui.pixelInset.width * 0.042f);  
		
		
		if (quest_complete == 0) {
			quest_1_gui.texture = incomplete_texture;
			quest_2_gui.texture = incomplete_texture;
		}
		else if (quest_complete == 1) {
			quest_1_gui.texture = complete_texture;
			quest_2_gui.texture = incomplete_texture;

		} else if (quest_complete == 2) {
			quest_1_gui.texture = incomplete_texture;
			quest_2_gui.texture = complete_texture;
			
		} else if (quest_complete == 3) {
			quest_1_gui.texture = complete_texture;
			quest_2_gui.texture = complete_texture;
		}

		if (quiz_complete == 0)
			quiz_gui.texture = incomplete_texture;
		else if (quiz_complete == 1)
			quiz_gui.texture = complete_texture;

	}
}
