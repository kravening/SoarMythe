using UnityEngine;
using System.Collections;


public class PauseMenu : MonoBehaviour {

	public string MainMenu;

	public bool isPaused;

	[SerializeField]
	CanvasGroup pauseMenuContainer;
	SaveLoad saveLoad;

	void Start() {
		//pauseMenuContainer.GetComponent<CanvasGroup> ();
		saveLoad = GetComponent<SaveLoad> ();

		if (isPaused) {
			Time.timeScale = 1.0f;
		} else {
			Time.timeScale = 0.0f;
		}
	}

	public void togglePause()
	{
		if (isPaused) {
			isPaused = false;
			Time.timeScale = 1.0f;
			pauseMenuContainer.alpha = 0;
			pauseMenuContainer.interactable = false;
			pauseMenuContainer.blocksRaycasts = false;
		} else {
			isPaused = true;
			Time.timeScale = 0.0f;
			pauseMenuContainer.alpha = 1;
			pauseMenuContainer.interactable = true;
			pauseMenuContainer.blocksRaycasts = true;
		}
	}
	public void mainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
	public void quit()
	{
		Application.Quit ();
	}

}
