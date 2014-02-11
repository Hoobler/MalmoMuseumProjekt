using UnityEngine;
using System.Collections;

public class LightMoving : MonoBehaviour {

	Vector3 lightPos;

	enum Direction{
		Up,
		Down,
		Left,
		Right,
		None

	}

	Direction direction;

	// Use this for initialization
	void Start () {
		lightPos = new Vector3 (-3, 0, -3);
		direction = Direction.Right;

	}
	
	// Update is called once per frame
	void Update () {
		LightController ();

	}

	void LightController(){
		if (direction == Direction.Left) {
			System.Console.WriteLine("Left");
			lightPos.x -= 0.01f;		
			if(lightPos.x <= -3)
				direction = Direction.Down;
		}
		
		else if (direction == Direction.Right) {
			System.Console.WriteLine("Right");
			lightPos.x += 0.01f;		
			if(lightPos.x >= 3){
				direction = Direction.Up;
				System.Console.WriteLine("Direction up");
			}
				
		}
		
		else if (direction == Direction.Down) {
			System.Console.WriteLine("Down");
			lightPos.z -= 0.01f;		
			if(lightPos.z <= -3)
				direction = Direction.Right;
		}
		
		else if (direction == Direction.Up) {
			System.Console.WriteLine("Up");
			lightPos.z += 0.01f;		
			if(lightPos.z >= 3)
				direction = Direction.Left;
		}

		transform.position = lightPos;
	}
}
