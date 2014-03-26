using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraRotation{
	public float MinRangeY;
	public float MaxRangeY;
	public float MinRangeX;
	public float MaxRangeX;
	public float time;
}

public class MovingSight : MonoBehaviour {

	public CameraRotation[] RotationMatrix;

	private Transform _cameraTransform;
	private Vector3 _rotationVector;
	private bool _active;
	private int _index;
	private float _timeLeft;

	void Start () {
		//_cameraTransform = Camera.main.transform;
		//_rotationVector = new Vector3(Random.Range(-20.0f,20.0f), Random.Range(-20.0f,20.0f), Camera.main.transform.rotation.z);
		_rotationVector = new Vector3(RotationMatrix[0].MinRangeX,
		                             RotationMatrix[0].MaxRangeX,
		                             Camera.main.transform.rotation.z);
		_active = true;
		StartCoroutine(SightSway());
	}

	void Update () {
		//Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(_rotationVector), Time.deltaTime);
	}	

	IEnumerator SightSway(){
		int index = 0;
		float timeLeft = 0;
		bool newTime = true;
		while(true){
			if(!_active){ yield return new WaitForSeconds(2.0f); }
			else{
				if(index > RotationMatrix.Length -1){
					index = 0;
				}
				if(newTime){
					timeLeft = RotationMatrix[index].time;
					newTime = false;
				}
				_rotationVector = new Vector3(RotationMatrix[index].MinRangeX,
				                              RotationMatrix[index].MaxRangeX,
				                              Camera.main.transform.rotation.z);

				Debug.Log(timeLeft);
				if(timeLeft >=0){;
					Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(_rotationVector), Time.deltaTime);
					timeLeft -= Time.deltaTime;
				}
				if(timeLeft <= 0){
					index++;
					newTime = true;
				}
				yield return null;
			}
		}
	}
}
