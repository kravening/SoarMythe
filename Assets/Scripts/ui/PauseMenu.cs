using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    string mainMenu;
    public string MainMenu {
        get { return mainMenu; }
        set { mainMenu = value; }
    }
	bool isPaused;
    public bool IsPaused {
        get { return isPaused; }
        set { isPaused = value; }
    }
    bool up, down = false;
    [SerializeField]
    List<GameObject> pauseButtons;
	CanvasGroup pauseMenuContainer;

    XboxInputMenu xboxInput;

    void Start() {
        xboxInput = GetComponent<XboxInputMenu>();
		//pauseMenuContainer.GetComponent<CanvasGroup> ();
		if (isPaused) {
			Time.timeScale = 1.0f;
		} else {
			Time.timeScale = 0.0f;
		}
	}
    void FixedUpdate()
    {
        control();

    }
    void control()
    {
        up = xboxInput.Up;
        down = xboxInput.Down;
    }

	public void TogglePause()
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
	public void LoadMenu()
	{
        // Load MainMenu
		SceneManager.LoadScene("MainMenu");
	}
	public void Quit()
	{
        // Quit the game
		Application.Quit ();
	}
}
