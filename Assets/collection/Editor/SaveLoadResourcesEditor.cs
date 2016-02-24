using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SaveLoadResources))]
public class SaveLoadResourcesEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		SaveLoadResources saveLoadResources = (SaveLoadResources)target;

		//DrawDefaultInspector ();
		EditorGUILayout.LabelField("Gold:", saveLoadResources.Gold.ToString());

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Save Game")) 
		{
			saveLoadResources.SaveResource ();
		}
		if (GUILayout.Button ("Load Game")) 
		{
			saveLoadResources.LoadResource ();
		}
		GUILayout.EndHorizontal ();
	}
}
