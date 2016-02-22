using UnityEngine;
using System.Collections;

public class TextFieldIngame : MonoBehaviour {

	public string text;
	bool display = false;

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.name == "Player") {
			display = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.transform.name == "Player") 
		{
			display = false;
		}
	}

	void OnGUI()
	{
		if (display == true) 
		{
			GUI.Box (new Rect(360,300,Screen.width-700, Screen.height-650), text);
		}
	}
}
