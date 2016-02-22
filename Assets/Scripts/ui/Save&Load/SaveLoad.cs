using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

	public void Save()
	{
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "saveFile.sav");

		SaveManager Saving = new SaveManager ();
		Saving.Power = dummyPowerscript.dPower.Power;
		//all other...



		binary.Serialize (fStream, Saving);
		fStream.Close ();


	}
	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/saveFile.sav")) 
		{
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "saveFile.sav", FileMode.Open);
			SaveManager saving = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();

			dummyPowerscript.dPower.Power = saving.Power;
			//all other...
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

[Serializable]
class SaveManager
{
	public int Power;
	//add stuff...
	//int, float, bool, string, v2/3/4, quaternions matrix 4x4 color rect layermask
	//unity engine.object = gameobject component monobehavior texture2d animationclips
	//enums array & list
}