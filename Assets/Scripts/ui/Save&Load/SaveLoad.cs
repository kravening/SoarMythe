using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour 
{

	public int LastCheckpoint;
	/*{
		get { LastCheckPoint; }
	}*/
	public int maxPower;
	/*{
	  get { MaxPower }
	}*/

	public void Save()
	{
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "saveFile.sav");

		SaveManager Saving = new SaveManager ();
		Saving.Checkpoint = LastCheckpoint;
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

			LastCheckpoint = saving.Checkpoint;
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
	public int Checkpoint;
	public int Powar;
	//add-able stuff...
	//int, float, bool, string, v2/3/4, quaternions matrix 4x4 color rect layermask
	//unity engine.object = gameobject component monobehavior texture2d animationclips
	//enums array & list
}