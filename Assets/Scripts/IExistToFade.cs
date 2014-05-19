using UnityEngine;
using System.Collections;

public class IExistToFade : MonoBehaviour {

	GUITexture image;
	float alpha;
	public float timeUntilFadeStart = 1f;
	public float totalFadeTime = 1f;
	float currentFadeTime = 80f;
	bool hasGUITexture;
	bool hasGUIText;
	// Use this for initialization
	void Start () {
		hasGUITexture = (gameObject.guiTexture != null);
		hasGUIText = (gameObject.guiText != null);
	}

	// Update is called once per frame
	void Update () {
		if(timeUntilFadeStart <= 0)
		{
			if(currentFadeTime<= 0)
			{
				Destroy (gameObject);
			}
			else
			{
				currentFadeTime-= Time.deltaTime;
				alpha = (currentFadeTime/totalFadeTime);
				if(hasGUITexture)
				{
					gameObject.guiTexture.color = new Color(gameObject.guiTexture.color.r, gameObject.guiTexture.color.g, gameObject.guiTexture.color.b, alpha);
				}
				if(hasGUIText)
				{
					gameObject.guiText.color = new Color(gameObject.guiText.color.r, gameObject.guiText.color.g, gameObject.guiText.color.b,alpha);
				}
			}	
		}
		else
		{
			timeUntilFadeStart -= Time.deltaTime;
			if(timeUntilFadeStart <= 0)
			   currentFadeTime = totalFadeTime;
		}

	}
}
