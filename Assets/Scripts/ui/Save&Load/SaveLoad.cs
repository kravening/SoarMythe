using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour 
{
    [SerializeField]


	GameObject finalCheckpoint;
    float currentPower;

	public void Save()
	{
        PowerContainer pc = GetComponent<PowerContainer>();
        CheckpointController cc = GetComponent<CheckpointController>();
		if (cc.LastCheckpoint != null) {
			finalCheckpoint = cc.LastCheckpoint;
		} else 
		{
			throw new Exception ("The player has no checkpoint");
		}
		currentPower = pc.MaxPower;
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "saveFile.sav");

		PlayerSave Saving = new PlayerSave ();
		Saving.Checkpoint = finalCheckpoint;
		Saving.Power = currentPower;
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
			PlayerSave saving = (PlayerSave)binary.Deserialize (fStream);

			finalCheckpoint = saving.Checkpoint;
			currentPower = saving.Power;
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
class PlayerSave
{
	public GameObject Checkpoint;
    public float Power;
	//add-able stuff...
	//int, float, bool, string, v2/3/4, quaternions matrix 4x4 color rect layermask
	//unity engine.object = gameobject component monobehavior texture2d animationclips
	//enums array & list
}