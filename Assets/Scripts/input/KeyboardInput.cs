using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInput : MonoBehaviour {
    PlayerMovement playerMovement;

	[SerializeField]
	PauseMenu pauseMenu;

    bool up, right, down, left, jump, glide, use, pause = false;

    [SerializeField]
    Vector2 mousePos;

    [SerializeField]
    Vector2 lastMousePos;

    public Vector2 MousePos {
        get { return mousePos; }
    }

	void Start(){
		playerMovement = gameObject.GetComponent<PlayerMovement>();
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

        SendCameraMovement();

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Prototype_V1");
        }

		playerMovement.Move(up, down, left, right, jump, glide);
	}

    void CheckKeys() {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        glide = Input.GetKey(KeyCode.Space);

        if (left && right) {
            left = right = false;
        }

        if (up && down) {
            up = down = false;
        }
    }

    void CheckKeysDown() {
		jump = Input.GetKeyDown(KeyCode.Space);
		use = Input.GetKeyDown(KeyCode.E);
    }

	void CheckSpecialKeys() {
		pause = Input.GetKeyDown (KeyCode.Escape);

		if (pause) {
			pauseMenu.togglePause ();
		}
	}

    void UpdateMousePos() {
        mousePos.x = Input.GetAxis("Horizontal");
        mousePos.y = Input.GetAxis("Vertical");

        mousePos -= lastMousePos;
    }

    void SendCameraMovement() {
        // Do things
    }
}
