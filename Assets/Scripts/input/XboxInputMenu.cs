using UnityEngine;
using XInputDotNetPure;

public class XboxInputMenu : MonoBehaviour {
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    //Project Specific (add vars for storing classes here)

    //behaviourModifiers
    //[SerializeField]
    float deadZoneAmount, triggerPressedSensitivity;

    //for reading
    bool LeftStickActive, RightStickActive;
    float LeftStickAngle, RightStickAngle, RightStickY, RightStickX, LeftStickY, LeftStickX;

    //bools for buttons
    bool leftShoulder, rightShoulder, leftTrigger, rightTrigger = false;

    [SerializeField]
    bool aButton, bButton, xButton, yButton = false;

    bool leftStickButton = false;

    MainMenuHandler mmh;

    [SerializeField]
    PauseMenu pauseMenu;

    //[SerializeField]
    bool up, down, left, right, pause = false;

    [SerializeField]
    bool dpadUp, dpadDown, dpadLeft, dpadRight = false;

    public bool Up {
        get { return dpadUp; }
    }
    public bool Down {
        get { return dpadDown; }
    }
    public bool Left {
        get { return dpadLeft; }
    }
    public bool Right {
        get { return dpadRight; }
    }

    void Start() {
        mmh = GetComponent<MainMenuHandler>();
    }

    void Update() {
        FindController();
        SetState();
        CheckForButtonPress();
        CheckForButtonRelease();

        PauseMenu();
        ProcessAndSendMovement();
    }

    void CheckForButtonPress() // check if a button was pressed this frame aka button down
    {

        if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed) {
            dpadLeft = true;
        }
        if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed) {
            dpadRight = true;
        }

        if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed) {
            dpadUp = true;

            mmh.MoveUp();
        }
        if (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed) {
            dpadDown = true;

            mmh.MoveDown();
        }

        // buttons
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
            aButton = true;

            if(mmh.InsideMenu)
                mmh.PressActionButton();
        }
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) {
            bButton = true;

            if(!mmh.InsideMenu)
                mmh.PressActionButton();
        }
    }
    void CheckForButtonRelease() // check if a button is released this frame
    {
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) {
            aButton = false;
        }
        if (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released) {
            bButton = false;
        }
        if (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released) {
            xButton = false;
        }
        if (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released) {
            yButton = false;
        }

        if (state.DPad.Left == ButtonState.Released && prevState.DPad.Left == ButtonState.Pressed) {
            dpadLeft = false;
        }
        if (state.DPad.Right == ButtonState.Released && prevState.DPad.Right == ButtonState.Pressed) {
            dpadRight = false;
        }
        if (state.DPad.Up == ButtonState.Released && prevState.DPad.Up == ButtonState.Pressed) {
            dpadUp = false;
        }
        if (state.DPad.Down == ButtonState.Released && prevState.DPad.Down == ButtonState.Pressed) {
            dpadDown = false;
        }
    }

    void PauseMenu()
    {
        //Input whatever...

        if (pause) {
            pauseMenu.togglePause();
        }
    }

    void FindController() {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected and use it
        if (!playerIndexSet || !prevState.IsConnected) {
            for (int i = 0; i < 4; ++i) {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected) {
                    //Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
    }

    void SetState() {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    void ProcessAndSendMovement() {
        up = down = left = right = false;

        if (LeftStickY > 0.001) {
            up = true;
        } else if (LeftStickY < -0.001) {
            down = true;
        }

        if (LeftStickX > 0.001) {
            right = true;
        } else if (LeftStickX < -0.001) {
            left = true;
        }

        if (left && right) {
            left = right = false;
        }

        if (up && down) {
            up = down = false;
        }
    }
}