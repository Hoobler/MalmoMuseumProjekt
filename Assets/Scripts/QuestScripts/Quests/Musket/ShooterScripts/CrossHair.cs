using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class CrossHair : MonoBehaviour {

	private GUITexture _guiTexture;

	// Use this for initialization
	void Start () {
		_guiTexture = GetComponent<GUITexture>();
		_guiTexture.enabled = false;
		EventManager.OnActivate += EventRespons;
	}

	void EventRespons(string type, ActiveEnum activeEnum){
		if(type == "CrossHair"){
			if(activeEnum == ActiveEnum.Active){
				_guiTexture.enabled = true;
			}
			else if(activeEnum == ActiveEnum.Disabled){
				_guiTexture.enabled = false;
			}
		}
	}
}
