﻿using UnityEngine;
using System.Collections;

public class DebugConsole : MonoBehaviour
{
	public string DebugText = string.Empty;

	void OnGUI ()
	{
		GUI.Label (new Rect (0, 0, 100, 30), DebugText);
	}
}