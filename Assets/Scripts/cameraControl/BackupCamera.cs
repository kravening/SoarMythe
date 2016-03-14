using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	const float X_ANGLE_MIN = -50.0f;
	const float X_ANGLE_MAX = 50.0f;

	public Transform lookAt;
	Transform camTransform;
	public float transitionDuration = 2.5f;
	Camera cam;

	public float distance = 8.0f;
	float currentX = 0.0f;
	float sensitivityX = 4.0f;

	void Start()
	{
		camTransform = transform;
		cam = Camera.main;
	}

	void Update()
	{
		currentX += Input.GetAxis ("Mouse X");

		currentX = Mathf.Clamp (currentX, X_ANGLE_MIN, X_ANGLE_MAX);
	}

	void LateUpdate()
	{
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (0, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);
	}
}