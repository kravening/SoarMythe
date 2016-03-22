using UnityEngine;

public class MoveCamera : MonoBehaviour {

	const float Y_ANGLE_MIN = 10.0f;
	const float Y_ANGLE_MAX = 50.0f;

	public Transform lookAt;
	Transform camTransform;
	public float transitionDuration = 2.5f;
	Camera cam;
    CameraControl camcontrol;
	public float distance = 8.0f;
	float currentX = 0.0f;
	float currentY = 0.0f;
	float sensitivityX = 4.0f;
	float sensitivityY = 1.0f;
    Vector3 targetPos;
    //{ get {  } }

    CameraControl cameraControl;

	void Start()
	{
		camTransform = transform;
        cam = transform.parent.gameObject.GetComponent<Camera>();
        cameraControl = GetComponent<CameraControl>();

    }

	void Update()
	{
        //targetPos = cameraControl.Tpos;
		currentX += Input.GetAxis ("Mouse X");
		currentY += Input.GetAxis ("Mouse Y");

		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	void LateUpdate()
	{
        //Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * targetPos;
		camTransform.LookAt (lookAt.position);
	}
}