using UnityEngine;
using System.Collections;
using System;

//The event enums are lockated in the EventEnums.cs

//The Delegates goes here! You need a delegate and an event for this to work.
public delegate void QuestEvent(QuestTypeEnum typeEnum, string stuff);
public delegate void FireEvent(int id);
public delegate void LockPlayerEvent(string type , LockPlayerEventArgs evtArgs);

public class EventManager : MonoBehaviour {

	//Create events to subscribe to!
	public static event QuestEvent OnQuest;
	public static event FireEvent OnHit;
	public static event LockPlayerEvent OnLock;

	//Set up a public methods that you can use to "trigger" an event!
	//Pass in the parameters you want/need for the event.

	/// <summary>
	/// Triggers the on quest.
	/// </summary>
	/// <param name="typeEnum">An enum for what type of quest event</param>
	/// <param name="type">String info about the event.</param>
	public static void TriggerOnQuest(QuestTypeEnum typeEnum, string type){
		if(OnQuest != null){
			OnQuest(typeEnum, type);
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
}
