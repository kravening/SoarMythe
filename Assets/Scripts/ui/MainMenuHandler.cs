using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuHandler : MonoBehaviour {

    [SerializeField]
    List<GameObject> MainMenuButtons;
    
    [SerializeField]
    int currentButton, lastButton = 0;

    XboxInputMenu xinput;

    [SerializeField]
    Transform startPoint;

    [SerializeField]
    List<Transform> creditsPath;

    [SerializeField]
    List<Transform> controlsPath;

    [SerializeField]
    Color selectedButtonColor = Color.red;

    [SerializeField]
    List<Transform> currentPath;

    [SerializeField]
    bool moveToNewPlace, up, down, reachedEndPath = false;

    bool insideMenu = true;

    Transform camera;

    float cameraSpeed = 0.2f;

    [SerializeField]
    float time;

    void Start() {
        xinput = GetComponent<XboxInputMenu>();
        camera = Camera.main.transform;

        SetColor(MainMenuButtons[currentButton], selectedButtonColor);
    }

    void FixedUpdate() {
        UpdateVariables();

        if (reachedEndPath) {
            reachedEndPath = false;
            insideMenu = true;
        } else {
            if (moveToNewPlace) {
                if (!insideMenu) {
                    time -= Time.fixedDeltaTime * cameraSpeed;
                } else {
                    time += Time.fixedDeltaTime * cameraSpeed;
                }

                if (time < 0) {
                    insideMenu = true;
                    moveToNewPlace = false;
                    time = 0;
                } else if (time > 1) {
                    insideMenu = false;
                    moveToNewPlace = false;
                    time = 1;
                }

                camera.position = Bezier2(startPoint, currentPath[0], currentPath[1], time);
            }
        }
    }

    // This function isn't actually used for anything. But it might be.
    /// <summary>
    /// Change the current button to a new value.
    /// </summary>
    /// <param name="newButton">Int of new button</param>
    void SetSelectedButton(int newButton) {
        // Set last to current to edit its color.
        lastButton = currentButton;

        // Set current to the new one.
        currentButton = newButton;

        // Then have this handle the changes.
        ButtonChangeHandling();
    }

    void UpdateVariables() {
        // Retrieve up and down from XboxInputMenu class.
        up = xinput.Up;
        down = xinput.Down;
    }

    /// <summary>
    /// Set last button then
    /// negate currentButton by one.
    /// Moving the selection up.
    /// </summary>
    public void MoveUp() {
        // Set last.
        lastButton = currentButton;

        // Edit current.
        currentButton--;

        // Have this check limits and edit colors.
        ButtonChangeHandling();
    }

    /// <summary>
    /// Set last button then
    /// add one to currentButton.
    /// Moving the selection down. 
    /// </summary>
    public void MoveDown() {
        // Set last.
        lastButton = currentButton;

        // Edit current.
        currentButton++;

        // Have this check limits and edit colors.
        ButtonChangeHandling();
    }

    void ButtonChangeHandling() {
        // Check if it's within limits.
        if (currentButton < 0) {
            currentButton = MainMenuButtons.Count - 1;
        }

        if (currentButton > MainMenuButtons.Count - 1) {
            currentButton = 0;
        }

        // Then edit the colors.
        SetColor(MainMenuButtons[currentButton], selectedButtonColor);
        SetColor(MainMenuButtons[lastButton], Color.white);
    }

    /// <summary>
    /// Set color of given button to color.
    /// </summary>
    /// <param name="button">Button to change color of.</param>
    /// <param name="color">Color to change button to.</param>
    void SetColor(GameObject button, Color color) {
        button.GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// Get Color of button.
    /// </summary>
    /// <param name="button">To retrieve color from.</param>
    /// <returns>Color retrieved from button.</returns>
    Color getColor(GameObject button) {
        return button.GetComponent<SpriteRenderer>().color;
    }

    /// <summary>
    /// Activate selected button.
    /// </summary>
    public void PressActionButton() {
        // Check which button and do their action.
        if (insideMenu) {
            switch (currentButton) {
                case 0:
                    SceneManager.LoadScene("Prototype_V1");
                    break;
                case 1:
                    currentPath = controlsPath;
                    break;
                case 2:
                    Application.Quit();
                    #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                    #endif
                    break;
                case 3:
                    currentPath = creditsPath;
                    break;
            }

            // If it's the options or credits button, set these to true.
            if (currentButton == 1 || currentButton == 3) {
                moveToNewPlace = true;
            }
        } else {
            moveToNewPlace = true;
        }
    }

    Vector3 Bezier2(Transform s, Transform p, Transform e, float t) {
        float rt = 1 - t;

        if (insideMenu) {
            if (t >= 1) {
                rt = 0;
                reachedEndPath = true;
            }
        } else {
            if (t <= 0) {
                rt = 1;
                reachedEndPath = true;
            }
        }

        camera.rotation = Quaternion.Lerp(s.rotation, e.rotation, t);
        return rt * rt * s.position + 2 * rt * t * p.position + t * t * e.position;

        // This is just a sample run for myself: 
        //
        // s = (1,1,1)
        // p = (7,0,0)
        // e = (5,1,1)
        // t = 0.5;
        //
        // rt = 1 - 0.5 = 0.5
        //
        // rt  * rt  *    s    + 2 * rt  *  t  *   p    +  t  *  t  *    e
        // 0.5 * 0.5 * (1,1,1) + 2 * 0.5 * 0.5 *(7,0,0) + 0.5 * 0.5 * (5,1,1)
        // (0.25,0.25,0.25) + (3.5,0,0) + (1.25,0.25,0.25)
        // (4,0.5,0.5)
    }
}
