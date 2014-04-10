using UnityEngine;
using System.Collections;

public class TouchCamera : TouchInput {

	public float RotationSpeed;
	public bool InvertPitch;
	public Transform player;

	private int _inverted;
	private float _pitch;
	private float _yaw;
	private Vector3 _oRotation;
	
	void Start () {
		_oRotation = Camera.main.transform.eulerAngles;
		_pitch = _oRotation.x;
		_yaw = _oRotation.y;
		if(InvertPitch){
			_inverted = -1;
		}
		else{
			_inverted = 1;
		}
	}

	public override void OnTouchBegan(){
		touchToCheck = TouchInput.currTouch;
	}

	public override void OnTouchMoved(){
		_pitch -= Input.GetTouch(touchToCheck).deltaPosition.y * RotationSpeed * _inverted * Time.deltaTime;
		_yaw += Input.GetTouch(touchToCheck).deltaPosition.x * RotationSpeed * _inverted * Time.deltaTime;

		//Limit so you won't break your back!
		_pitch = Mathf.Clamp(_pitch, -80, 80);

		//Add the rotation to the camera!
		Camera.main.transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
	}

	public override void OnTouchEnded(){
		if(TouchInput.currTouch == touchToCheck || Input.touches.Length <= 0)
			touchToCheck = 64;
		if (TouchInput.currTouch == touchToCheck) {
			EventManager.TriggerOnTouchEvent(TouchEnum.Touched);
		}
	}
}
