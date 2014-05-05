using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject pcController;
	public GameObject androidController;

	private GameObject temp;
	private Vector3 spawnPos;
	//private CreateDynamicContainer container;

	void Start () {

		//Hittar spawn pointen i världen
		GameObject spawnObj = GameObject.Find("PlayerSpawn");
		//GameObjectet som fps controlen ska parentas till
		GameObject parent = GameObject.FindGameObjectWithTag("DynamicObjects");
		spawnPos = spawnObj.transform.position;
	#if UNITY_STANDALONE_WIN
		temp = Instantiate(pcController, spawnPos, Quaternion.identity) as GameObject;
	#endif
	#if UNITY_ANDROID
		temp = Instantiate(androidController, new Vector3(0,0,0), Quaternion.identity) as GameObject;
	#endif

		if (parent == null) {
			Debug.Log ("Parent object is null, creating a new object!");
			CreateDynamicContainer.CreateContainer ("DynamicObjects", "DynamicObjects");
			parent = GameObject.FindGameObjectWithTag("DynamicObjects");
			temp.transform.parent = parent.transform;
		} else if (!parent) {
			temp.transform.parent = parent.transform;
		}
	}

	void Update(){
		//Respawnar spelaren ifall han är under vatten på slottet
		#if UNITY_STANDALONE_WIN		
		//if (temp.transform.position.y < 3.5f && Application.loadedLevel == 3)
		//	temp.transform.position = spawnPos;
		#endif
		#if UNITY_ANDROID
//		if (temp.transform.position.y < 3.5f && Application.loadedLevel == 3)
//			temp.transform.position = spawnPos;
		#endif

	}
}
