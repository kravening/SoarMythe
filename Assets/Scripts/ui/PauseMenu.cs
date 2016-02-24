using UnityEngine;
using System.Collections;


public class PauseMenu : MonoBehaviour {

	public string MainMenu;
	public string Options;

	public bool isPaused;
	public GameObject pauseMenuCanvas;

	void Update()
	{
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0.0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1.0f;
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused; 
		}
	}
	public void resume()
	{
		isPaused = false;
	}
	public void saveGame()
	{
		GetComponent<SaveLoad> ().Save ();
	}
	public void load()
	{
		GetComponent<SaveLoad> ().Load ();
	}
	public void mainMenu()
	{
		Application.LoadLevel (MainMenu);
	}
	public void quit()
	{
		Application.Quit ();
	}

}
