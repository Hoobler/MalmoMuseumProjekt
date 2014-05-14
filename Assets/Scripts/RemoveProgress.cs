using System.Collections;
using UnityEngine;

public class RemoveProgress : MonoBehaviour {

	void OnMouseDown(){
		Destroy (GameObject.Find ("GotoButton"));
		Destroy (GameObject.Find ("Background"));
		Destroy (GameObject.Find ("Quest1"));
		Destroy (GameObject.Find ("Quest2"));
		Destroy (GameObject.Find ("Quiz"));
	}
}
