using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

	public static int currTouch;
	[HideInInspector]
	public int touchToCheck;

	void Start () {
		currTouch = 0;
		touchToCheck = 0;
	}

	void Update () {

		//Lägg till så den kan kolla om du träffar en guiTexture senare.
		//Så går det att använda för allt!
		if(Input.touches.Length == 0){
			DoNothing();
		}
		else{
			for(int i = 0; i < Input.touchCount; i++){
				currTouch = i;
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					OnTouchBegan();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Moved){
					OnTouchMoved();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Ended){
					OnTouchEnded();
				}
				if(Input.GetTouch(i).phase == TouchPhase.Stationary){
					OnTouchStationary();
				}
			}
		}
	}

	public virtual void DoNothing(){}
	public virtual void OnTouchBegan(){}
	public virtual void OnTouchMoved(){}
	public virtual void OnTouchEnded(){}
	public virtual void OnTouchStationary(){}
}
