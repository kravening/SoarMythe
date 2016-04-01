using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string MainMenu;
	public bool isPaused;

	[SerializeField]
	CanvasGroup pauseMenuContainer;

	void Start() {
		//pauseMenuContainer.GetComponent<CanvasGroup> ();
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
		SceneManager.LoadScene("MainMenu");
	}
	public void quit()
	{
		Application.Quit ();
	}

}
