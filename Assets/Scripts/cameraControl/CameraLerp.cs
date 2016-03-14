using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour 
{
	/*public float timeTakenDuringLerp = 1f;
	public float distanceToMove = -10;
	bool isLerping;

	CameraControl cameraControl;

	Vector3 startPosition;
	Vector3 endPosition;
	float timeStartedLerping;
	// Update is called once per frame
	void Update () 
	{
		TempPick();
	}
	void Start()
	{
		cameraControl = GetComponent<CameraControl> ();
	}
	void TempPick()
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			cameraControl.enabled = false;
			StartLerping ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			cameraControl.distance = 15.0f;
			cameraControl.enabled = true;
		}
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			cameraControl.distance = 8.0f;
			cameraControl.enabled = true;
		}
	}
	void FixedUpdate()
	{
		if (isLerping) 
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			if (percentageComplete >= 1.0f) 
			{
				isLerping = false;
			}
		}
	}

	void StartLerping()
	{
		isLerping = true;
		timeStartedLerping = Time.time;

		startPosition = transform.position;
		endPosition = transform.position + Vector3.forward * distanceToMove;
	}*/
}
