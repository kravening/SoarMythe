using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour 
{

	public Transform finalCheckpoint;

	public int currentPower;

	public void Save()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement> ();
		if (playerMovement.LastCheckpoint != null) {
			finalCheckpoint = playerMovement.LastCheckpoint;
		} else {
			throw new Exception ("The player has no checkpoint");
		}
		currentPower = GetComponent<PlayerMovement> ().MaxPower;
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "saveFile.sav");

		SaveManager Saving = new SaveManager ();
		Saving.Checkpoint = finalCheckpoint;
		Saving.Powar = currentPower;
		//other...

		binary.Serialize (fStream, Saving);
		fStream.Close ();
		Debug.Log ("Saved");

	}
	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/saveFile.sav")) 
		{
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "saveFile.sav", FileMode.Open);
			SaveManager saving = (SaveManager)binary.Deserialize (fStream);

			finalCheckpoint = saving.Checkpoint;
			currentPower = saving.Powar;
			//other...

			fStream.Close ();
			Debug.Log ("Loaded");
		}
	}
	public void Delete()
	{
		if(File.Exists(Application.persistentDataPath + "/saveFile.sav"))
		{
			File.Delete(Application.persistentDataPath + "/saveFile.sav");
		}
	}
}

[System.Serializable]
class SaveManager
{
	public Transform Checkpoint;
	public int Powar;
	//add-able stuff...
	//int, float, bool, string, v2/3/4, quaternions matrix 4x4 color rect layermask
	//unity engine.object = gameobject component monobehavior texture2d animationclips
	//enums array & list
}