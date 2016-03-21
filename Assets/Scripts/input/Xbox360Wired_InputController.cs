using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Xbox360Wired_InputController : MonoBehaviour {
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	//Objects to Affect
	[SerializeField]PlayerMovement player;
	[SerializeField]CameraControl cam;

	//Project Specific (add vars for storing classes here)

    //behaviourModifiers
    [SerializeField]float deadZoneAmount;
    [SerializeField]float triggerPressedSensitivity;

    //for reading
    public float LeftStickAngle;
    public float RightStickAngle;
    public bool RightStickActive;
    public bool LeftStickActive;
    public float LeftStickX;
    public float LeftStickY;
    public float RightStickX;
    public float RightStickY;

    //bools for buttons
    bool leftShoulder = false;
    bool rightShoulder = false;
    bool leftTrigger = false;
    bool rightTrigger = false;

    bool aButton = false;
    bool bButton = false;
    bool xButton = false;
    bool yButton = false;

    bool aButtonHeld = false;
    bool bButtonHeld = false;
    bool xButtonHeld = false;
    bool yButtonHeld = false;

	bool leftStickButton = false;

    bool forward, backward, left, right, jump, glide = false;
    bool camLeft, camRight, camUp, camDown = false;

    // Use this for initialization
    void Start () {
		cam = GameObject.FindGameObjectWithTag (Tags.MAIN_CAMERA).GetComponent<CameraControl>();
        player = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		FindController ();
        SetState();
        CheckForButtonPress();
        CheckForButtonRelease();
        ButtonActions(); //do things like shooting here

        ProcessAndSendMovement();

        if (DeadZoneCheckRight())
        {
            RightStickActive = true;
            RightStickX = state.ThumbSticks.Right.X;//holds x value of stick
            RightStickY = state.ThumbSticks.Right.Y;//holds y value of stick
            RightStickAngle = CalculateRotation(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y); // calculates a angle for the right stick
        }
        else
        {
            RightStickActive = false;
            RightStickX = 0f; // set it back to 0 if inside the deadzone
            RightStickY = 0f; // set it back to 0 if inside the deadzone
        }
        if (DeadZoneCheckLeft())
        {
            LeftStickActive = true;
            LeftStickX = state.ThumbSticks.Left.X;//holds x value of stick
            LeftStickY = state.ThumbSticks.Left.Y;//holds y value of stick
            LeftStickAngle = CalculateRotation(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);   // calculates a angle for the left stick
        }
        else
        {
            LeftStickActive = false;
            LeftStickX = 0f; // set it back to 0 if inside the deadzone
            LeftStickY = 0f; // set it back to 0 if inside the deadzone
        }

    }
    void ButtonActions()
    {
		if (player) {
            // Button keys
		}
    }
    void CheckForButtonPress() // check if a button was pressed this frame aka button down
    {
        //shoulders
		if (player) {
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



			// buttons
			if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
				aButton = true;
                jump = true;
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
	}
	void CheckForButtonRelease() // check if a button is released this frame
    {
		if (player) {
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
		}
	}
	
	public bool DeadZoneCheckRight()
    {
        if (state.ThumbSticks.Right.X >= deadZoneAmount || state.ThumbSticks.Right.X <= -deadZoneAmount || state.ThumbSticks.Right.Y >= deadZoneAmount || state.ThumbSticks.Right.Y <= -deadZoneAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeadZoneCheckLeft()
    {
        if (state.ThumbSticks.Left.X >= deadZoneAmount || state.ThumbSticks.Left.X <= -deadZoneAmount || state.ThumbSticks.Left.Y >= deadZoneAmount || state.ThumbSticks.Left.Y <= -deadZoneAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FindController(){
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected and use it
		if (!playerIndexSet || !prevState.IsConnected) {
			for (int i = 0; i < 4; ++i) {
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState (testPlayerIndex);
				if (testState.IsConnected) {
					Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}
    }
    void SetState()
    {
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
        if (LeftStickY > 0.001) {
            forward = true;
        } else if (LeftStickY < -0.001) {
            backward = true;
        }

        if (LeftStickX > 0.001) {
            right = true;
        } else if (LeftStickX < -0.001) {
            left = true;
        }

        if (RightStickX > 0.001) {
            camRight = true;
        } else if (RightStickX < -0.001) {
            camLeft = true;
        }

        if (RightStickY > 0.001) {
            camUp = true;
        } else if (RightStickY < -0.001) {
            camDown = true;
        }

        if (jump && glide) {
            glide = false;
        }

        player.Move(forward, backward, left, right, jump, glide);

        if (camLeft)
            cam.RotateY(-1);
        else if(camRight)
            cam.RotateY(1);

        if (camUp)
            cam.RotateX(1);
        else if (camDown)
            cam.RotateX(-1);

        forward = backward = left = right = jump = glide = false;
        camLeft = camRight = camUp = camDown = false;
    }
}
