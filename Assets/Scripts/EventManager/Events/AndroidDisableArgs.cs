using UnityEngine;
using System.Collections;
using System;

public class AndroidDisableArgs : EventArgs {

	private bool _left;
	private bool _right;

	public bool Left{
		get{ return _left; }
		set{ _left = value; }
	}

	public bool Right{
		get{ return _right; }
		set{ _right = value; }
	}

}
