using UnityEngine;
using System.Collections;

public class LevelLoadTest : MonoBehaviour {
	public string levelToLoad;

	private GUITexture overlay_gui;
	private GUITexture overlay_text_gui;

	void OnMouseDown(){
        if (this.gameObject.name == "NyttSpel")
            PlayerPrefs.DeleteAll();

		GameObject overlay = new GameObject ("Overlay");
		GameObject overlay_text = new GameObject ("OverlayText");

		
		overlay_gui = (GUITexture)overlay.AddComponent (typeof(GUITexture));
		overlay_gui.texture = (Texture)Resources.Load("loading_overlay");
		overlay_gui.transform.localScale = Vector3.zero;
		overlay_gui.transform.position = new Vector3 (0, 0, 2);
		overlay_gui.pixelInset = new Rect (0, 0, Screen.width, Screen.width);

		overlay_text_gui = (GUITexture)overlay_text.AddComponent (typeof(GUITexture));
		overlay_text_gui.texture = (Texture)Resources.Load ("loading_text");
		overlay_text_gui.transform.localScale = Vector3.zero;
		overlay_text_gui.transform.position = new Vector3 (0, 0, 3);
		overlay_text_gui.pixelInset = new Rect (Screen.width * 0.7f, 0 , Screen.width * 0.3f, Screen.height * 0.2f);


		Application.LoadLevel(levelToLoad);
	}
}
