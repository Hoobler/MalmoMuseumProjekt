using UnityEngine;
using System.Collections;
using System;

public class AndroidDisableArgs : EventArgs {

	private bool _disable;

	public bool Disable{
		get{ return _disable; }
		set{ _disable = value; }
	}
}
