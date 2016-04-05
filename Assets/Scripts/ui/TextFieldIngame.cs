using UnityEngine;
using System.Collections;

public class TextFieldIngame : MonoBehaviour {

	public string text;
	bool display = false;

	void OnTriggerEnter(Collider col)
	{
        // Display text for player
		if (col.transform.name == "Player") {
			display = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
        // Stop displaying the text
		if (col.transform.name == "Player") 
		{
			display = false;
		}
	}

	void OnGUI()
	{
        // Create the text
		if (display == true) 
		{
			GUI.Box (new Rect(360,300,Screen.width-700, Screen.height-650), text);
		}
	}
}
