using UnityEngine;
using System.Collections;
using System;

public class QuestEventArgs : EventArgs {
	
	public readonly QuestTypeEnum QuestType;
	public readonly MiniGamesEnum MiniGames;
	public readonly string Info;
	public readonly int Points;
	
	public QuestEventArgs(MiniGamesEnum game ,QuestTypeEnum type){
		this.QuestType = type;
		this.MiniGames = game;
	}

	public QuestEventArgs(MiniGamesEnum game, QuestTypeEnum type, string info){
		this.QuestType = type;
		this.MiniGames = game;
		this.Info = info;
	}
}
