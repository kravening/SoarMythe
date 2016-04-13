using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;


	}
}
