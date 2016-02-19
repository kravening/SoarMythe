using UnityEngine;
using System.Collections;
//Dylan Waij

public class cameraSwap : MonoBehaviour {

	public Camera camera;
	public Camera camera2;

	void Start() {
		camera.enabled = true;
		camera2.enabled = false;
	}
	void Update() {
		//This will toggle the cameras, it is not efficient but was made purely to be workable.
		if (Input.GetKeyDown (KeyCode.F1)) {
			camera.enabled = true;
			camera2.enabled = false;
		} 
		if (Input.GetKeyDown (KeyCode.F2)) {
			camera.enabled = false;
			camera2.enabled = true;
		}
	 }
}