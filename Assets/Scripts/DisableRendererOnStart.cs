using UnityEngine;
using System.Collections;

public class DisableRendererOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
}
