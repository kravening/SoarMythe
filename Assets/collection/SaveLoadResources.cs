using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadResources : MonoBehaviour 
{

	[SerializeField]private int gold;

	public int Gold
	{
		get { return gold; }
	}
	public void SaveResource()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "SaveData.Sav");

		SaveData saveData = new SaveData ();
		saveData.gold = gold;

		bf.Serialize (file, saveData);
		file.Close ();
		Debug.Log ("Resources Saved");
	}
	public void LoadResource()
	{
		if (File.Exists (Application.persistentDataPath + "/SaveData.sav")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/SaveData.sav", FileMode.Open);
			SaveData saveData = (SaveData)bf.Deserialize (file);
			gold = saveData.gold;
			Debug.Log ("Resources Loaded");
		}
	} 

}
[System.Serializable]
public class SaveData
{
	public int gold;
}