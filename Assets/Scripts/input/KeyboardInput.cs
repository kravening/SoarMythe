using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    PlayerMovement pm;

	[SerializeField]
	PauseMenu pauseMenu;

    [SerializeField]
    bool up, right, down, left, jump, glide, pause, restart = false;

    [SerializeField]
    Vector2 mouseSensitivity = new Vector2(1, 1);

    Vector2 mousePosChanges, lastMousePos;

    [SerializeField]
    float mouseSensitivityDivider = 2;

    CameraControl cc;

    PowerContainer pc;

    public Vector2 MousePos {
        get { return mousePosChanges; }
    }

	void Start(){
		pm = gameObject.GetComponent<PlayerMovement>();
        cc = Camera.main.GetComponent<CameraControl>();
        pc = GetComponent<PowerContainer>();

        if (cc == null)
            Debug.LogError("No CameraControl was found in the camera!");
        if (pm == null)
            Debug.LogError("Player does not contain a PlayerMovement!");
	}

	void Update() {
		CheckSpecialKeys ();
	}

	void FixedUpdate () {
		// Check keys
        CheckKeys();
        CheckKeysDown();

        // Get Mouse Axis
        UpdateMousePos();

        if(cc != null)
            SendCameraMovement();

        if (restart && GameController.RequestSceneName() != "MainMenu")
            GameController.RestartCurrentScene();

        if (pm.IsAlive)
            pm.Move(up, down, left, right, jump, glide);

	}

    void CheckKeys() {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        //glide = Input.GetKey(KeyCode.E);
		if (Input.GetKeyDown (KeyCode.E)) {
			glide = true;
		}
		if (Input.GetKeyUp (KeyCode.E)) {
			glide = false;
		}
        if (left && right) {
            left = right = false;
        }

        if (up && down) {
            up = down = false;
        }
    }

    void CheckKeysDown() {
		jump = Input.GetKeyDown(KeyCode.Space);
        restart = Input.GetKeyDown(KeyCode.R);
    }

	void CheckSpecialKeys() {
		pause = Input.GetKeyDown(KeyCode.Escape);

		if (pause)
			pauseMenu.TogglePause();

	}

    void UpdateMousePos() {
        mousePosChanges.x = Input.mousePosition.x;
        mousePosChanges.y = Input.mousePosition.y;

        mousePosChanges -= lastMousePos;

        lastMousePos = Input.mousePosition;
    }

    void SendCameraMovement() {
        // Rotate X is up and down.
        // So mousesensitivity X and mousePosChanges on Y since that's up and down.
        cc.RotateX((mouseSensitivity.x / mouseSensitivityDivider) * mousePosChanges.y);

        // Rotate Y is left and right.
        // So the Y of mousesensitivity and X of mousePosChanges because that's left and right.
        cc.RotateY((mouseSensitivity.y / mouseSensitivityDivider) * mousePosChanges.x);
    }
}
