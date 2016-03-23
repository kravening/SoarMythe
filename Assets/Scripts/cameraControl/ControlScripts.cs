﻿using UnityEngine;
using System.Collections;

public class ControlScripts : MonoBehaviour {

	CameraControl scriptReference1;
	MoveCamera scriptReference2;
    XboxInputMenu xboxInput;
    bool xboxActive;
    
	void Start()
	{
        xboxInput = GetComponent<XboxInputMenu>();
        scriptReference1 = GetComponent<CameraControl> ();
		scriptReference2 = GetComponent<MoveCamera> ();
	}
	void Update()
	{
		if (xboxActive == true) {
			scriptReference1.enabled = false;
			scriptReference2.enabled = true;
		} else {
			scriptReference1.enabled = true;
			scriptReference2.enabled = false;
		}
	}
}
