using UnityEngine;
using System.Collections;

public class QuestBase : MonoBehaviour {

	public virtual void TriggerStart(){


	}

	public virtual void TriggerFinish(){

				GameObject backgroundMain = new GameObject ("Quest End Confirmation Window");
				GUITexture backTex = (GUITexture)backgroundMain.AddComponent (typeof(GUITexture));

				backTex.texture = Resources.Load ("bild") as Texture;
		DestroyOnClick doc = (DestroyOnClick)backgroundMain.AddComponent (typeof(DestroyOnClick));
		doc.enabled = true;
		backTex.transform.localScale = new Vector3 (0.5f, 0.5f, 0);

				GameObject textObject = new GameObject ("Text");
				
				GUIText text = (GUIText)textObject.AddComponent (typeof(GUIText));
				text.text = "Grattis, du är inte dum i huvudet";
		text.transform.Translate(new Vector3 (0, 0, 0.1f));
		textObject.AddComponent (typeof(DestroyOnClick));
		textObject.transform.parent = backgroundMain.transform;
				backTex.transform.Translate (new Vector3 (0.5f, 0.5f, 0));

		}
}
