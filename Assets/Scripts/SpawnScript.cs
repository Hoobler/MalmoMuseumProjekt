using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject pcController;
	public GameObject androidController;

	void Start () {
		//Later!
		GameObject spawnObj = GameObject.FindGameObjectWithTag("SpawnPoint");
		GameObject parent = GameObject.FindGameObjectWithTag("DynamicObjects");
		Vector3 spawnPos = spawnObj.transform.position;
		GameObject temp = Instantiate(pcController, spawnPos, Quaternion.identity) as GameObject;

		if(parent != null){
		temp.transform.parent = parent.transform;
		}

	#if UNITY_ANDROID
		//Instantiate(androidController, spawnPos, Quaternion.identity);
	#endif
	}
}
