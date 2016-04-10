using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    bool isPaused, isShowing;
    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }

    [SerializeField]
    CanvasGroup pauseMenuContainer;
    GameObject CheckCanvas;

    void Start()
    {
        pauseMenuContainer.GetComponent<CanvasGroup> ();
        CheckCanvas.GetComponent<GameObject>();
        isShowing = false;
        CheckCanvas.SetActive(isShowing);

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
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void Quit()
    {
        Application.Quit();
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}