using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PauseMenu : MonoBehaviour {

    bool Esc, select;

    [SerializeField]
    CanvasGroup pauseMenuContainer;
    GameObject CheckCanvas;

    void Update()
    {
        CheckIfPaused();
    }
    void CheckIfPaused()
    {
        if (Esc = Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            pauseMenuContainer.alpha = 1;
            pauseMenuContainer.interactable = true;
            pauseMenuContainer.blocksRaycasts = true;
        }
    }
    public void TogglePause()
    {
            Time.timeScale = 1.0f;
            pauseMenuContainer.alpha = 0;
            pauseMenuContainer.interactable = false;
            pauseMenuContainer.blocksRaycasts = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}