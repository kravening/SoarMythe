using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuItems 
{
	[MenuItem("Tools/Clear PlayerPrefs")]
	public static void DeletePlayerPrefs()
	{
		PlayerPrefs.DeleteAll ();
		Debug.Log ("PlayerPrefs cleared");
	}
}
