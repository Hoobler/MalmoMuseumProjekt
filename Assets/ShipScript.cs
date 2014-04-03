using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

	GameObject startPoint;
	GameObject endPoint;
	Vector3 distance;
	Vector3 speed;

	// Use this for initialization
	void Start () {

		startPoint 	= GameObject.Find ("ShipStart");
		endPoint 	= GameObject.Find ("ShipEnd");

		distance = endPoint.transform.position - startPoint.transform.position;
		speed = new Vector3 (0.05f, 0, 0.05f);

		gameObject.transform.position = startPoint.transform.position;

		distance.Normalize();

	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += new Vector3 (distance.x * speed.x, speed.y, distance.z * speed.z);
	}

	public Vector3 Speed{
		get{return speed;}
		set{speed = value;}
	}
}
