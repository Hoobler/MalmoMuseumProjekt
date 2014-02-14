using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	public ChangeTargetColor script;

	void Start () {
	
	}

	void Update () {
		RaycastHit hit;
		Transform cam = Camera.main.transform;
		Ray ray = new Ray(cam.position, cam.forward);

		if(Input.GetMouseButtonDown(0)){
			if(Physics.Raycast(ray ,out hit, 100f)){

				GameObject otherObj = hit.collider.gameObject;
				//Debug ray för att se vart din ray ens går!
				Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 5f); 

				if(otherObj.tag == "Target"){
					script = (ChangeTargetColor) otherObj.GetComponent(typeof(ChangeTargetColor));
					script.ChangeSize();
				}
			}
		}
 	}
}
