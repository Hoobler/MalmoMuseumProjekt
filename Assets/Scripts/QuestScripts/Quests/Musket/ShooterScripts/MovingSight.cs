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

	public bool TestThis;
	public CameraRotation[] RotationMatrix;

	private Transform _cameraTransform;
	private Vector3 _rotationVector;
	private bool _active;
	private int _index;
	private float _timeLeft;

	void Start () {
//		EventManager.OnQuest += EventRespons;
		StartCoroutine(SightSway());
	}

	IEnumerator SightSway(){
//		int index = 0;
//		float timeLeft = 0;
//		bool newTime = true;
		while(true){
			if(!_active){ yield return new WaitForSeconds(2.0f); }
			else{
				float t = Time.time;
				float y = Mathf.Sin(t) * 20.0f;
				float x = Mathf.Cos(t* 3) * 10.0f;
				_rotationVector = new Vector3(x, y, 0.0f);

				Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation,(Camera.main.transform.rotation * Quaternion.Euler(_rotationVector)), Time.deltaTime * 0.2f);

				yield return null;
			}
		}
	}
	//lol!
	void EventRespons(MiniGamesEnum miniEnum , QuestEventArgs evArgs){
		if(miniEnum == MiniGamesEnum.Musköt){
			if(evArgs.QuestType == QuestTypeEnum.Started){
				_active = true;
			}
			if(evArgs.QuestType == QuestTypeEnum.Finnished){
				_active = false;
			}
		}
	}
}
