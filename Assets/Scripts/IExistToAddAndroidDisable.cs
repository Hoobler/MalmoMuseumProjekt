using UnityEngine;
using System.Collections;

public class IExistToAddAndroidDisable : MonoBehaviour {

	float timeLeftToLive = 0.2f;

	// Update is called once per frame
	void Update () {
		if(timeLeftToLive<= 0)
		{
			Debug.Log ("I am dead.");
			gameObject.AddComponent<AndroidDisable>();
			Destroy (this);
		}
		else
			timeLeftToLive-= Time.deltaTime;
	}
}
