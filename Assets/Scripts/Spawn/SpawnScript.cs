using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject pcController;
	public GameObject androidController;

	void Start () {

		//Hittar spawn pointen i världen
		GameObject spawnObj = GameObject.FindGameObjectWithTag("SpawnPoint");
		//GameObjectet som fps controlen ska parentas till
		GameObject parent = GameObject.FindGameObjectWithTag("DynamicObjects");
		Vector3 spawnPos = spawnObj.transform.position;
	#if UNITY_STANDALONE_WIN
		GameObject temp = Instantiate(pcController, spawnPos, Quaternion.identity) as GameObject;
	#endif
	#if UNITY_ANDROID
		GameObject temp = Instantiate(androidController, spawnPos, Quaternion.identity) as GameObject;
	#endif

		if (parent != null) {
			temp.transform.parent = parent.transform;
		} else if (parent == null) {
			Debug.Log("Parent object is null");
		}
	}
}
