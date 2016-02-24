using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    PlayerMovement playerMovement;

	[SerializeField]
	PauseMenu pauseMenu;

    bool forward, right, backward, left, jump, glide, use, pause = false;

	void Start(){
		playerMovement = gameObject.GetComponent<PlayerMovement> ();
	}	

	void Update() {
		CheckSpecialKeys ();
	}

	void FixedUpdate () {
		//Check keys
        CheckKeys();
        CheckKeysDown();

		playerMovement.Move(forward, backward, left, right, jump, glide);
	}

    void CheckKeys() {
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        glide = Input.GetKey(KeyCode.Space);

        if (left && right) {
            left = right = false;
        }

        if (forward && backward) {
            forward = backward = false;
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
}
