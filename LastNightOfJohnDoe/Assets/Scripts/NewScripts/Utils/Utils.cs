using UnityEngine;
using System.Collections;

public static class Logger {
	
	public static void Log(string message)
	{
		Debug.Log("<color=green>" + message + "</color>");
	}

	public static void Log(string message, Color color)
	{
		Debug.Log("<color=" + color + ">" + message + "</color>");
	}
}
