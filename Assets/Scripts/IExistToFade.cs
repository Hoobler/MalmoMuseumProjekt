using UnityEngine;
using System.Collections;

public class IExistToFade : MonoBehaviour {

	GUITexture image;
	float alpha;

	// Use this for initialization
	void Start () {
		image = ((GUITexture)gameObject.GetComponent (typeof(GUITexture)));
		alpha = image.color.a;
	}
	
	// Update is called once per frame
	void Update () {
		alpha -= Time.deltaTime;
		if (alpha <= 0)
						Destroy (gameObject);
				else
						image.color = new Color (image.color.r, image.color.g, image.color.b, alpha);
	}
}
