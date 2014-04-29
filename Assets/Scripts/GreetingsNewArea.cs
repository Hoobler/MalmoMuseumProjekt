using UnityEngine;
using System.Collections;

public class GreetingsNewArea : MonoBehaviour {

    private float alpha;

//    private float fadeAlpha = 0.0f;
//    private bool fadeIn = true;

    private Color color;

    GUIText mainGUI;
    public GUITexture signTexture;
	void Start () {


	
	}
	 
	void Update () {

        //if (fadeIn)
        //{
        //    fadeAlpha += 0.01f;
        //    Debug.Log(fadeAlpha);
        //    Debug.Log(signTexture.color);
        //    if (fadeAlpha >= 1.0f)
        //        fadeIn = false;
        //}
        //else fadeAlpha -= 0.01f;


	}

    void OnGUI()
    {
        color.a = Mathf.Lerp(color.a, 1, 0.1f * Time.deltaTime);

        Debug.Log(color.a);

        signTexture.color = color;

        GUI.DrawTexture(new Rect(10, 10, 60, 60), signTexture.texture);
    }
}
