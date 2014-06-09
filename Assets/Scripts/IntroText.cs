using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {

    public Rect ScreenInset;
    public int fontSize;

	// Use this for initialization
	void Start () {
        guiTexture.pixelInset = new Rect(Screen.width * ScreenInset.x, Screen.height * ScreenInset.y, Screen.width * ScreenInset.width, Screen.height * ScreenInset.height);
        guiText.pixelOffset = new Vector2(guiTexture.GetScreenRect().x + guiTexture.GetScreenRect().width * 0.1f, guiTexture.GetScreenRect().y + guiTexture.GetScreenRect().height*0.9f);
        guiText.fontSize = (int)(fontSize * Screen.width / 800f);
        FormatMainText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    void FormatMainText()
    {
        string[] text = guiText.text.Split(' ');
        guiText.text = "";

        for (int i = 0; i < text.GetLength(0); i++)
        {

            guiText.text += text[i] + " ";
            if (guiText.GetScreenRect().width > guiTexture.GetScreenRect().width * 0.8f)
            {
                guiText.text = guiText.text.Substring(0, guiText.text.Length - text[i].Length - 1);
                guiText.text += "\n" + text[i] + " ";
            }
        }
    }
}
