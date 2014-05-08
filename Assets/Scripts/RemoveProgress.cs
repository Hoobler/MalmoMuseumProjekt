using System.Collections;
using UnityEngine;

public class RemoveProgress : MonoBehaviour {

	void OnMouseDown(){
		Destroy (GameObject.Find ("GotoButton"));
		Destroy (GameObject.Find ("Background"));
	}
}
