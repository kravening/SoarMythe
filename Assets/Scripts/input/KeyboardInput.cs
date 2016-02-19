using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {
    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    bool forward, right, backward, left, jump, glide = false;

	void Update () {
        CheckKeys();
        CheckKeysDown();
	}

    void CheckKeys() {
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        jump = Input.GetKeyDown(KeyCode.Space);
        glide = Input.GetKey(KeyCode.Space);

        if (left && right) {
            left = right = false;
        }

        if (forward && backward) {
            forward = backward = false;
        }

        playerMovement.Move(forward, backward, left, right, jump, glide);
    }

    void CheckKeysDown() {
        if (Input.GetKeyDown(KeyCode.E)) {

        }
    }
}
