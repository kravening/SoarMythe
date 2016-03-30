using UnityEngine;
using System.Collections.Generic;

public class MainMenuHandler : MonoBehaviour {

    [SerializeField]
    List<GameObject> MainMenuButtons;

    GameObject currentButton;

    XboxInputMenu xinput;

    [SerializeField]
    bool left, right, up, down = false;

    void Start() {
        xinput = GetComponent<XboxInputMenu>();

        currentButton = MainMenuButtons[0];

        ChangeColor(currentButton, Color.red);
    }

    void Update() {
        UpdateVariables();

        ChangeColor(currentButton, Color.red);
    }

    void SetSelectedButton(GameObject newButton) {
        ChangeColor(currentButton, Color.white);

        currentButton = newButton;

        ChangeColor(currentButton, Color.red);
    }

    void UpdateVariables() {
        left = xinput.Left;
        right = xinput.Right;
        up = xinput.Up;
        down = xinput.Down;
    }

    void ChangeColor(GameObject button, Color color) {
        button.GetComponent<SpriteRenderer>().color = color;
    }
}
