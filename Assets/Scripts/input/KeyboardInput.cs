using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    PlayerMovement playerMovement;

    [SerializeField]
    bool forward, right, backward, left, jump, glide, use = false;
	void Start(){
		playerMovement = gameObject.GetComponent<PlayerMovement> ();
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
}
