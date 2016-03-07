using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//Vector3 position = Camera.main.transform.position;
	public Transform lookAt;
	Transform camTransform;
	Camera cam;

	public float CamTargetHeight = 1.0f;
	public float distance = 8.0f;
	float currentX = 0.0f;
	float sensitivityX = 4.0f;
	Vector3 dir = new Vector3 (-0.26f, 3.44f, -8.37f);

	float XboxControlX;
	float raycastDistance = 15f;
	float spherecastDistance = 5f;
	float sphereRadius = 2f;
	float speedToFixClipping = 0.2f;

	void Start()
	{
		XboxControlX = GetComponent<Xbox360Wired_InputController> ().RightStickX;
		camTransform = transform;
		cam = Camera.main;
	}
	void Update()
	{
		currentX += Input.GetAxis ("Mouse X");
		//currentX += xboxControlX;
	}
	void LateUpdate()
	{
		Quaternion rotation = Quaternion.Euler (0, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);
	}/*
	void fixClippingThroughWalls()
	{
		RaycastHit hit;
		Vector3 direction = transform.parent.position - transform.position;
		Vector3 localPos = transform.localPosition;

		for (float i = offset.z; i <= 0f; i += speedToFixClipping) 
		{
			Vector3 pos = transform.TransformPoint (new Vector3 (localPos.x, localPos.y, i));
			if (Physics.Raycast (pos, direction, out hit, raycastDistance)) 
			{
				if (!hit.collider.CompareTag ("Player"))
					continue;
				if (!Physics.SphereCast (pos, sphereRadius, transform.forward * -1, out hit, spherecastDistance)) 
				{
					localPos.z = i;
					break;
				}
			}
		}
		transform.localPosition = Vector3.Lerp (transform.localPosition, localPos, smoothing * Time.deltaTime);
	}*/
}