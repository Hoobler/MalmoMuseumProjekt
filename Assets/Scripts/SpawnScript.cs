using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public Transform pcController;
	public Transform androidController;
	//public GameObject spawnObj;

	void Start () {
		//Later!
		GameObject spawnObj = GameObject.FindGameObjectWithTag("SpawnPoint");
		Vector3 spawnPos = spawnObj.transform.position;

		Instantiate(pcController, spawnPos, Quaternion.identity);

	#if UNITY_ANDROID
		//Instantiate(androidController, spawnPos, Quaternion.identity);
	#endif
	}
}
