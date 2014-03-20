using UnityEngine;
using System.Collections;
using System;

public class QuestEventArgs : EventArgs {
	
	public QuestTypeEnum QuestType;
	public string Info;
	public int Points;
	
	public QuestEventArgs(QuestTypeEnum type, string info){
		this.QuestType = type;
		this.Info = info;
	}
}
