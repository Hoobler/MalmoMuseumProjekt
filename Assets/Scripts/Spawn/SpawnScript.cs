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
		GameObject temp = Instantiate(pcController, spawnPos, Quaternion.identity) as GameObject;

		if (parent != null) {
						temp.transform.parent = parent.transform;
		} else if (parent == null) {
			Debug.Log("Parent object is null");
		}
	#if UNITY_ANDROID
		//Instantiate(androidController, spawnPos, Quaternion.identity);
	#endif
	}
}
