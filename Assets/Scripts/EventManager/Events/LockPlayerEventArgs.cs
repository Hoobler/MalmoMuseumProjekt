using UnityEngine;
using System.Collections;
using System;

public class LockPlayerEventArgs : EventArgs {

	public Transform transform;
	public Transform lookAt;

	public LockPlayerEventArgs(Transform transform){
		this.transform = transform;
	}

	public LockPlayerEventArgs(Transform transform, Transform lookAt){
		this.transform = transform;
		this.lookAt = lookAt;
	}
}
