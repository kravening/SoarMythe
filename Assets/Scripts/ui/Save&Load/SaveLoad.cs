using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Save()
	{
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create (Application.persistentDataPath + "saveFile.sav");

		SaveManager Saving = new SaveManager ();
		Saving.Power = dummyPowerscript.dPower.Power;
		//all other...



		binary.Serialize (fStream, Saving);
		fStream.Close ();


	}
	void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/saveFile.sav")) 
		{
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "saveFile.sav", FileMode.Open);
			SaveManager saving = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();
		}
	}
	void Delete()
	{
		
	}
}

[Serializable]
class SaveManager
{
	public int Power;
	//add stuff...
}