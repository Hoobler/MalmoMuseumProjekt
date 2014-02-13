using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	public ChangeTargetColor script;

	void Start () {
	
	}

	void Update () {
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(ray ,out hit, 100f)){

				GameObject otherObj = hit.collider.gameObject;

				if(otherObj.tag == "Target"){
					script = (ChangeTargetColor) otherObj.GetComponent(typeof(ChangeTargetColor));
					script.ChangeSize();
				}
			}
		}
 	}
}
