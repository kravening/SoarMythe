using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string MainMenu;
	public bool isPaused;

	[SerializeField]
	CanvasGroup pauseMenuContainer;

    XboxInputMenu xboxInputPause;

    void Start() {
        xboxInputPause = GetComponent<XboxInputMenu>();
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
            // Set's false and time continue's again
			isPaused = false;
			Time.timeScale = 1.0f;
			pauseMenuContainer.alpha = 0;
			pauseMenuContainer.interactable = false;
			pauseMenuContainer.blocksRaycasts = false;
		} else {
            // Set's true and stops time
			isPaused = true;
			Time.timeScale = 0.0f;
			pauseMenuContainer.alpha = 1;
			pauseMenuContainer.interactable = true;
			pauseMenuContainer.blocksRaycasts = true;
		}
	}
	public void mainMenu()
	{
        // Load MainMenu
		SceneManager.LoadScene("MainMenu");
	}
	public void quit()
	{
        // Quit the game
		Application.Quit ();
	}

}
