using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour 
{
	public float timeTakenDuringLerp = 1f;
	public float distanceToMove = -10;
	private bool isLerping;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private float timeStartedLerping;
	// Update is called once per frame
	void Update () 
	{
		//ControlCamera ();
		TempPick();
	}
	private void TempPick()
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			GetComponent<CameraControl> ().enabled = false;
			StartLerping ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			GetComponent<CameraControl> ().distance = 15.0f;
			GetComponent<CameraControl> ().enabled = true;
		}
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			GetComponent<CameraControl> ().distance = 8.0f;
			GetComponent<CameraControl> ().enabled = true;
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
	}
}
/*
	IEnumerator LerpandWait()
	{
		while (GetComponent<CameraControl> ().transform.position  18) 
		{
			isLerping = true;
			timeStartedLerping = Time.time;

			startPosition = transform.position;
			endPosition = transform.position + Vector3.forward * distanceToMove;
			yield return null;
		}
		print ("Reached location");
		yield return new WaitForSeconds (2f);
		print ("Courontine finished");
		//GetComponent<CameraControl> ().distance = 18.0f;
	}
	private void ControlCamera()
	{// Will change to triggers or something else.
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			GetComponent<CameraControl> ().enabled = false;
			StartCoroutine (LerpandWait());
			//GetComponent<CameraControl> ().enabled = true;
		}
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			GetComponent<CameraControl> ().distance = 8.0f;
		}
	}*/
