using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour 
{

	public Transform finalCheckpoint;

	public int currentPower;

	public bool checkPos(){
		if (GetComponent<PlayerMovement> ().LastCheckpoint != null) {
			finalCheckpoint.transform.position = GetComponent<PlayerMovement> ().LastCheckpoint.transform.position;
		} else {
			GetComponent<PlayerMovement>().LastCheckpoint = null;
		}
	}

	public void Save()
	{
		checkPos ();
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