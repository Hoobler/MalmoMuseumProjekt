using UnityEngine;
using System.Collections;
using System;

public delegate void QuestEvent();
public delegate void FireEvent(int id);
public delegate void LockPlayerEvent(string type , LockPlayerEventArgs evtArgs);

public class EventManager : MonoBehaviour {

	public static event QuestEvent OnQuest;
	public static event FireEvent OnHit;
	public static event LockPlayerEvent OnLock;

	public static void TriggerOnQuest(){
		if(OnQuest != null){
			Debug.Log("TriggerOnQuest");
			OnQuest();
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

	public static void TriggerLockPlayer(string type, LockPlayerEventArgs evtArgs){
		if(OnLock != null){
			OnLock(type, evtArgs);
		}
	}
}
