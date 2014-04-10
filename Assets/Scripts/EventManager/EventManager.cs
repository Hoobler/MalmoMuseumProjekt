using UnityEngine;
using System.Collections;
using System;

//The event enums are lockated in the EventEnums.cs

//The Delegates goes here! You need a delegate and an event for this to work.
public delegate void ActivateEvent(string type, ActiveEnum activeEnum);
public delegate void QuestEvent(MiniGamesEnum miniEnum ,QuestEventArgs eventArgs);
public delegate void FireEvent(int id);
public delegate void LockPlayerEvent(string type , LockPlayerEventArgs evtArgs);
public delegate void DisableAndroid(string type);
public delegate void TouchEvent(TouchEnum touchEnum);

public class EventManager : MonoBehaviour {

	//Create events to subscribe to!
	public static event ActivateEvent OnActivate;
	public static event QuestEvent OnQuest;
	public static event FireEvent OnHit;
	public static event LockPlayerEvent OnLock;
	public static event DisableAndroid OnDisable;
	public static event TouchEvent OnTouchEvent;

	//Set up a public methods that you can use to "trigger" an event!
	//Pass in the parameters you want/need for the event.

	/// <summary>
	/// Triggers the on quest.
	/// </summary>
	/// <param name="typeEnum">An enum for what type of quest event</param>
	/// <param name="type">String info about the event.</param>
	public static void TriggerOnQuest(MiniGamesEnum miniEnum ,QuestEventArgs eventArgs){
		if(OnQuest != null){
			OnQuest(miniEnum, eventArgs);
		}
	}

	public static void TriggerOnHit(int id){
		if(OnHit != null){
			OnHit(id);
		}
	}

	public static void TriggerShootSpot(int id){
		if(OnHit != null){
			OnHit(id);
		}
	}
	// Kommer antagligen tabort!
	public static void TriggerLockPlayer(string type, LockPlayerEventArgs evtArgs){
		if(OnLock != null){
			OnLock(type, evtArgs);
		}
	}

	public static void TriggerOnActivate(string type, ActiveEnum activeEnum){
		if(OnActivate != null){
			OnActivate(type, activeEnum);
		}
	}

	public static void TriggerDisableAndroid(string type){
		if(OnDisable != null){
			OnDisable(type);
		}
	}

	public static void TriggerOnTouchEvent(TouchEnum touchEnum){
		if(OnTouchEvent != null){
			OnTouchEvent(touchEnum);
		}
	}
}
