using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour {

	public void OnMouseDown(){
		Destroy(this);
	}
}
