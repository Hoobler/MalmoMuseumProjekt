﻿using UnityEngine;
using System.Collections;

public class MusketTarget : MonoBehaviour {
	
	public float Speed = 2f;
	public Transform[] triggers;

	private bool _questRunning;

	void Start () {
		_questRunning = false;
		EventManager.QuestEvent += new QuestHandler(MoveTargetEvent);
		StartCoroutine(MoveTarget(triggers[0].position, triggers[1].position));
	}

	//New ska stoppa target från att röra sig när questen inte är aktiv
	//Resten av tiden sak den åka mellan leftPos och rightPos
	IEnumerator MoveTarget(Vector3 leftPos, Vector3 rightPos){
		float distLeft, distRight;
		float rate = 0;
		bool goLeft = false;
		bool goRight = true;
		Vector3 newLeft = new Vector3(leftPos.x, transform.position.y, leftPos.z);
		Vector3 newRight = new Vector3(rightPos.x, transform.position.y, rightPos.z);
		while(true){
			if(!_questRunning){
				yield return new WaitForSeconds(0.5f);
			} else if(_questRunning) {
				distLeft = Vector3.Distance(this.transform.position, newLeft);
				distRight = Vector3.Distance(this.transform.position, newRight);
				rate = Time.deltaTime * Speed;

				if(distRight <= 0.01f){
					goLeft = true;
					goRight = false;
				}

				if(distLeft <= 0.01f){
					goLeft = false;
					goRight = true;
				}

				if(goRight){
					this.transform.position = Vector3.MoveTowards(transform.position, newRight, rate);
					yield return null;
				}
				if(goLeft){
					this.transform.position = Vector3.MoveTowards(transform.position, newLeft, rate);
					yield return null;
				}
			}
		}
	}
	
	void MoveTargetEvent(object o ,QuestEventArgs e){
		if(e.MiniGames == MiniGamesEnum.Musköt){
			if(e.QuestType == QuestTypeEnum.Started){
				_questRunning = true;
			}
			if(e.QuestType == QuestTypeEnum.Finnished){
				_questRunning = false;
			}
		}
	}
}