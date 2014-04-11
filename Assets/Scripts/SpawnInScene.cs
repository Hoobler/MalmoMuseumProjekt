using UnityEngine;
using System.Collections;

public class SpawnInScene : MonoBehaviour {

	GUITexture background;

	Texture background_texture;

	private bool fadein = true;
	private bool fadeout = false;
	private bool timerstart = false;
	private float alpha = 0.0f;

	private const float TIME_ON_SCREEN = 4.0f;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {

		if (Application.loadedLevel == 0)	//LILLA TORG
			background_texture = Resources.Load ("skylt_lillatorg") as Texture;

		if(Application.loadedLevel == 3)	//SLOTTET
			background_texture = Resources.Load ("skylt_slottet") as Texture;

		GameObject backObject = new GameObject ();
		background = (GUITexture)backObject.AddComponent (typeof(GUITexture));
		background.texture = background_texture;
		background.transform.position =  new Vector3 (0.5f, 0.7f, 0);
		background.transform.localScale = new Vector3 (0.4f, 0.15f, 0);

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (fadein) {
			alpha += 0.005f;
			if(alpha >= 1.0f)
				fadein = false;
		}
		if (fadeout) {
			Debug.Log ("Fade out");
			alpha -= 0.005f;
			if(alpha <= 0.0f){
				fadeout = false;
				//Tar bort scriptet så det inte updaterar mer.
				Destroy(gameObject.GetComponent("SpawnInScene"));
			}
		}

		if (timer > TIME_ON_SCREEN) {
			fadein = false;
			fadeout = true;
		}

		background.color = new Color (background.color.r, background.color.g, background.color.b, alpha);
	}
	
}
