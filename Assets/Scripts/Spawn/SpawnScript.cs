using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject pcController;
	public GameObject androidController;

	//private CreateDynamicContainer container;

	void Start () {

		//Hittar spawn pointen i världen
		GameObject spawnObj = GameObject.Find("PlayerSpawn");
		//GameObjectet som fps controlen ska parentas till
		GameObject parent = GameObject.FindGameObjectWithTag("DynamicObjects");
		Vector3 spawnPos = spawnObj.transform.position;
	#if UNITY_STANDALONE_WIN
		GameObject temp = Instantiate(pcController, spawnPos, Quaternion.identity) as GameObject;
	#endif
	#if UNITY_ANDROID
		GameObject temp = Instantiate(androidController, new Vector3(0,0,0), Quaternion.identity) as GameObject;
	#endif

		if (parent == null) {
			Debug.Log ("Parent object is null, creating a new object!");
			CreateDynamicContainer.CreateContainer ("DynamicObjects", "DynamicObjects");
			parent = GameObject.FindGameObjectWithTag("DynamicObjects");
			temp.transform.parent = parent.transform;
		} else if (!parent) {
			temp.transform.parent = parent.transform;
		}




//		if (parent != null) {
//			temp.transform.parent = parent.transform;
//		} else if (parent == null) {
//			Debug.Log("Parent object is null");
//		}
	}
}
