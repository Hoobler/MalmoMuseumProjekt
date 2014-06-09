using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class CrossHair : MonoBehaviour {

	private GUITexture _guiTexture;

	// Use this for initialization
	void Start () {
		//_guiTexture = GameObject.Find("GUI_Crosshair").guiTexture;
		EventManager.OnActivate += EventRespons;
	}

	void EventRespons(string type, ActiveEnum activeEnum){
		if(type == "CrossHair"){
			Debug.Log("Crosshair");
			if(activeEnum == ActiveEnum.Active){
				GameObject.Find("GUI_Crosshair").guiTexture.enabled = true;
			}
			else if(activeEnum == ActiveEnum.Disabled){
				GameObject.Find("GUI_Crosshair").guiTexture.enabled = false;
			}
		}
	}
}
