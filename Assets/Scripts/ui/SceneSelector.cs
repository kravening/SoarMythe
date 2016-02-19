using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSelector : MonoBehaviour 
{
	public string loadLevel;

	public void ChangeLevel()
	{
		SceneManager.LoadScene(loadLevel);
	}
}
