using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour {
    string mainMenu;
    public string MainMenu
    {
        get { return mainMenu; }
        set { mainMenu = value; }
    }
    bool isPaused;
    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }

    [SerializeField]
    CanvasGroup pauseMenuContainer;

    void Start()
    {
        pauseMenuContainer.GetComponent<CanvasGroup> ();

        if (isPaused)
        {
            Time.timeScale = 1.0f;
        }
        else {
            Time.timeScale = 0.0f;
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            pauseMenuContainer.alpha = 0;
            pauseMenuContainer.interactable = false;
            pauseMenuContainer.blocksRaycasts = false;
        }
        else {
            isPaused = true;
            Time.timeScale = 0.0f;
            pauseMenuContainer.alpha = 1;
            pauseMenuContainer.interactable = true;
            pauseMenuContainer.blocksRaycasts = true;
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }

}





/*    string mainMenu;
    public string MainMenu {
        get { return mainMenu; }
        set { mainMenu = value; }
    }
	bool isPaused;
    public bool IsPaused {
        get { return isPaused; }
        set { isPaused = value; }
    }
    bool up, down, insidePause = false;
    int currentButton, lastButton = 0;

    [SerializeField]
    Color highlightButtonColor = Color.red;
    [SerializeField]
    List<GameObject> pauseButtons;
	CanvasGroup pauseMenuContainer;
    

    XboxInputMenu xboxInput;
    public bool InsidePause
    {
        get { return insidePause; }
    }

    void Start() {
        xboxInput = GetComponent<XboxInputMenu>();
		pauseMenuContainer.GetComponent<CanvasGroup> ();
		if (isPaused) {
			Time.timeScale = 1.0f;
		} else {
			Time.timeScale = 0.0f;
		}
        ButtonChangeHandling();
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
    public void MoveUp()
    {
        // Set last.
        lastButton = currentButton;

        // Edit current.
        currentButton--;

        // Have this check limits and edit colors.
        ButtonChangeHandling();
    }
    public void MoveDown()
    {
        // Set last.
        lastButton = currentButton;

        // Edit current.
        currentButton++;

        // Have this check limits and edit colors.
        ButtonChangeHandling();
    }
    void ButtonChangeHandling()
    {
        // Check if it's within limits.
        if (currentButton == -10)
        {
            // Do nothing, just need this for mouse.
        }
        else if (currentButton < 0)
        {
            currentButton = pauseButtons.Count - 1;
        }

        if (currentButton > pauseButtons.Count - 1)
        {
            currentButton = 0;
        }

        // Then edit the colors.
        if (lastButton >= 0)
        {
            SetColor(pauseButtons[lastButton], Color.white);
        }
        if (currentButton >= 0)
        {
            SetColor(pauseButtons[currentButton], highlightButtonColor);
        }
    }
    void SetColor(GameObject button, Color color)
    {
        button.GetComponent<SpriteRenderer>().color = color;
    }
    Color getColor(GameObject button)
    {
        return button.GetComponent<SpriteRenderer>().color;
    }
    public void PressActionButton()
    {
        // Check which button and do their action.
            switch (currentButton)
            {
                case 0:
                    isPaused = false;
                    break;
                case 1:
                    SceneManager.LoadScene("MainMenu");
                    break;
                case 2:
                    Application.Quit();
                    #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                    #endif
                    break;
            }
    }
}*/
