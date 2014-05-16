using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    GUITexture fortsatt;
    GUITexture nyttspel;
    GUITexture titel;
    public Texture greyfortsatt;

	// Use this for initialization
	void Start () {
        fortsatt = (GUITexture)GameObject.Find("Fortsatt").GetComponent(typeof(GUITexture));
        nyttspel = (GUITexture)GameObject.Find("NyttSpel").GetComponent(typeof(GUITexture));
        titel = (GUITexture)GameObject.Find("Titel").GetComponent(typeof(GUITexture));

        fortsatt.pixelInset = new Rect(Screen.width * 0.375f, Screen.height * 0.25f, 0.3f * Screen.width, 0.125f * Screen.height);
        nyttspel.pixelInset = new Rect(Screen.width * 0.36f, Screen.height * 0.12f, 0.3f * Screen.width, 0.125f * Screen.height);
        titel.pixelInset = new Rect(Screen.width * 0.25f, Screen.height * 0.55f, 0.5f * Screen.width, 0.375f * Screen.height);

        //(fortsatt.texture.width / 2)

        int checkLT, checkMHS, checkGB;
        checkLT = PlayerPrefs.GetInt("LTquest");
        checkMHS = PlayerPrefs.GetInt("Squest");
        checkGB = PlayerPrefs.GetInt("Gquest");

        if (checkLT == 0 && checkMHS == 0 && checkGB == 0)
        {
            fortsatt.texture = greyfortsatt;
        }

        
	}

    
}
