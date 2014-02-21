using UnityEngine;
using System.Collections;

public class CreateDynamicContainer : MonoBehaviour {
	public static void CreateContainer(string name, string tag){
		GameObject temp;
		if (!GameObject.FindGameObjectWithTag("DynamicObjects")) {
			temp = new GameObject (name);
			temp.tag = tag;
		}
	}

	public static void DestroyContainer(string name){
		Destroy (GameObject.Find(name));
	}
}
