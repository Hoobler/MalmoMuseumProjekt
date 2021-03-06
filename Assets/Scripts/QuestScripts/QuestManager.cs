﻿using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	bool questInProgress = false;

	public void ActivateQuest(string name)
	{
		if(!questInProgress)
		{
			questInProgress = true;
			if (name == "LillaTorgBagQuest") {
				QuestBase tact = GameObject.FindGameObjectWithTag ("Player").GetComponent ("TriggerActivation") as QuestBase;
				tact.TriggerStart();
			}
			
			if (name == "MusketQuest"){
				Debug.Log("QuestManager MusketQuest");
				QuestEventArgs qEvArgs = new QuestEventArgs(MiniGamesEnum.Musköt ,QuestTypeEnum.Started);
				EventManager.OnQuestEvent(qEvArgs);
			}
			
			if (name == "LillaTorgAppleQuest") {
				QuestBase apple = GameObject.Find ("AppleQuestPerson").GetComponent ("ThrowQuest") as QuestBase;
				apple.TriggerStart();
			}
			
			if (name == "GraBroderDiceGame") {
				QuestBase dicequest = GameObject.Find ("QuestGiverDice").GetComponent("DiceQuest") as QuestBase;
				dicequest.TriggerStart();
			}
			
			if (name == "SlottetCanonQuest") {
				QuestBase canonquest = GameObject.Find ("QuestGiverCanon").GetComponent("CanonQuest") as QuestBase;
				canonquest.TriggerStart();
			}
			
			if(name == "GraBroderSpion") {
				((QuestBase) GameObject.Find ("QuestGiverSpion").GetComponent("SpionQuest")).TriggerStart();
			}
		}
	}

	public void QuestFinished()
	{
		questInProgress = false;
	}

	public bool IsQuestInProgress()
	{
		return questInProgress;
	}

}
