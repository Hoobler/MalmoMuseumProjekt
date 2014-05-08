using UnityEngine;
using System.Collections;

public class ShowProgress : MonoBehaviour {

	private GUITexture goto_texture_gui;
	private GUITexture background_texture_gui;

	private int quest_complete;
	private int quiz_complete;

	public Texture goto_texture;
	public Texture background_texture;

	void OnMouseDown(){
		Destroy (GameObject.Find ("GotoButton"));
		Destroy (GameObject.Find ("Background"));
		ShowSceneInfo ();
	}

	private void ShowSceneInfo(){
		GameObject goto_button = new GameObject ("GotoButton");
		GameObject background = new GameObject ("Background");

		background_texture_gui = (GUITexture)background.AddComponent (typeof(GUITexture));
		background_texture_gui.texture = background_texture;
		background_texture_gui.transform.localScale = Vector3.zero;
		background_texture_gui.transform.position = Vector3.zero;
		background_texture_gui.pixelInset = new Rect ((float)gameObject.guiTexture.GetScreenRect ().x, (float)gameObject.guiTexture.GetScreenRect ().y, 0.2f * Screen.width, 0.2f * Screen.width);  

		goto_texture_gui = (GUITexture)goto_button.AddComponent (typeof(GUITexture));
		goto_texture_gui.texture = goto_texture;
		goto_texture_gui.transform.localScale = Vector3.zero;
		goto_texture_gui.transform.position = new Vector3 (0, 0, 1);
		goto_texture_gui.pixelInset = new Rect ((float)background_texture_gui.GetScreenRect ().x + background_texture_gui.pixelInset.width * 0.1f,
		                                        (float)background_texture_gui.GetScreenRect ().y - background_texture_gui.pixelInset.height * 0.25f, 
		                                        background_texture_gui.pixelInset.width * 0.8f, 
		                                        background_texture_gui.pixelInset.height * 0.2f); 

		LevelLoadTest load = goto_button.AddComponent<LevelLoadTest> ();
		load.levelToLoad = gameObject.name;


		//quest complete - 	0 - inga 
		//					1 - äpple 
		//					2 - bag 
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
	}
}
