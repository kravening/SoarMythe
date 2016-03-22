using UnityEngine;
using System.Collections;

public class PlayerMovementCameraPosition : MonoBehaviour {

    Transform camera;

    // Since I'm constantly needing a Quaternion to edit, I'll just have one present in memory that will be reused instead.
    Quaternion rotation;

    void Start() {
        camera = Camera.main.GetComponent<Transform>();
        rotation = new Quaternion();
    }

	void Update () {
        transform.position = camera.position; // Set my position to the camera's.
        rotation = camera.rotation; // Retrieve the camera's rotation to edit before I apply it.
        rotation.x = rotation.z = 0; // I only need the Y, so just set X and Z to 0.
        transform.rotation = rotation; // Then finally change my rotation to what I made.
	}
}
