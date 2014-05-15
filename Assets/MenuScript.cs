using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    GUITexture fortsatt;
    GUITexture nyttspel;
    GUITexture titel;

	// Use this for initialization
	void Start () {
        fortsatt = (GUITexture)GameObject.Find("Fortsatt").GetComponent(typeof(GUITexture));
        nyttspel = (GUITexture)GameObject.Find("NyttSpel").GetComponent(typeof(GUITexture));
        titel = (GUITexture)GameObject.Find("Titel").GetComponent(typeof(GUITexture));

        fortsatt.pixelInset = new Rect(Screen.width * 0.1f, Screen.height * 0.4f, 0.23f * Screen.width, 0.09f * Screen.height);
        nyttspel.pixelInset = new Rect(Screen.width * 0.1f, Screen.height * 0.25f, 0.27f * Screen.width, 0.09f * Screen.height);
        titel.pixelInset = new Rect(Screen.width * 0.5f, Screen.height * 0.55f, 0.47f * Screen.width, 0.38f * Screen.height);

        
	}

    
}
