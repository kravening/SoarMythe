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
        ButtonActions();

        PauseMenu();
        ProcessAndSendMovement();

        if (DeadZoneCheckRight()) {
            RightStickX = state.ThumbSticks.Right.X;//holds x value of stick
            RightStickY = state.ThumbSticks.Right.Y;//holds y value of stick
            RightStickAngle = CalculateRotation(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y); // calculates a angle for the right stick
        } else {
            RightStickX = 0f; // set it back to 0 if inside the deadzone
            RightStickY = 0f; // set it back to 0 if inside the deadzone
        }
        if (DeadZoneCheckLeft()) {
            LeftStickX = state.ThumbSticks.Left.X;//holds x value of stick
            LeftStickY = state.ThumbSticks.Left.Y;//holds y value of stick
            LeftStickAngle = CalculateRotation(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);   // calculates a angle for the left stick
        } else {
            LeftStickX = 0f; // set it back to 0 if inside the deadzone
            LeftStickY = 0f; // set it back to 0 if inside the deadzone
        }

    }
    void ButtonActions() {
        // Actions. 
    }
    void CheckForButtonPress() // check if a button was pressed this frame aka button down
    {
        //shoulders
        if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed) {
            leftShoulder = true;
        }
        if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed) {
            rightShoulder = true;
        }
        if (prevState.Triggers.Left >= triggerPressedSensitivity && leftTrigger == false) {
            leftTrigger = true;
        }
        if (prevState.Triggers.Right >= triggerPressedSensitivity && rightTrigger == false) {
            rightTrigger = true;
        }

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

            mmh.PressActionButton();
        }
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) {
            bButton = true;
        }
        if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) {
            xButton = true;
        }
        if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed) {
            yButton = true;
        }

        if (prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed) {
            leftStickButton = true;
        }
    }
    void CheckForButtonRelease() // check if a button is released this frame
    {
        //shoulders
        if (prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released) {
            leftShoulder = false;
        }
        if (prevState.Buttons.RightShoulder == ButtonState.Pressed && state.Buttons.RightShoulder == ButtonState.Released) {
            rightShoulder = false;
        }

        if (prevState.Triggers.Left <= triggerPressedSensitivity && leftTrigger == true) {
            leftTrigger = false;
        }
        if (prevState.Triggers.Right <= triggerPressedSensitivity && rightTrigger == true) {
            rightTrigger = false;
        }

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

        if (prevState.Buttons.LeftStick == ButtonState.Pressed && state.Buttons.LeftStick == ButtonState.Released) {
            leftStickButton = false;
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

    public bool DeadZoneCheckRight() {
        if (state.ThumbSticks.Right.X >= deadZoneAmount || state.ThumbSticks.Right.X <= -deadZoneAmount || state.ThumbSticks.Right.Y >= deadZoneAmount || state.ThumbSticks.Right.Y <= -deadZoneAmount) {
            return true;
        } else {
            return false;
        }
    }

    public bool DeadZoneCheckLeft() {
        if (state.ThumbSticks.Left.X >= deadZoneAmount || state.ThumbSticks.Left.X <= -deadZoneAmount || state.ThumbSticks.Left.Y >= deadZoneAmount || state.ThumbSticks.Left.Y <= -deadZoneAmount) {
            return true;
        } else {
            return false;
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

    float CalculateRotation(float X, float Y) // calculates angle based on incoming X & Y values;
    {
        float angle = (Mathf.Atan2(X, Y) * Mathf.Rad2Deg);
        //Debug.Log(angle);
        return angle;
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