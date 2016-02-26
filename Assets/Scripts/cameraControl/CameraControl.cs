using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform lookAt;
	Transform camTransform;
	Camera cam;

	public float distance = 8.0f;
	float currentX = 0.0f;
	float sensitivityX = 4.0f;
	Vector3 dir = new Vector3 (-0.26f, 3.44f, -8.37f);

	/*public Transform toFollow;
	float distanceToPlayer;
	Vector3 toFollowLastPos;
	Vector3 deltaPos
	{ get { return toFollow.position - toFollowLastPos; }  } 
	float height
	{ get { return toFollow.position.y - transform.position.y; } } 
	float distanceOnXZ
	{ get { return Mathf.Sqrt(Mathf.Pow(distanceToPlayer, 2) - Mathf.Pow(height, 2)); } }
	Rigidbody rigidbody;
	bool isColliding = false;*/

	void OnCollisionEnter(){
	RaycastHit hit;
	// Calculate Ray direction
	Vector3 direction = Camera.main.transform.position - transform.position; 
	if(Physics.Raycast(transform.position, direction, out hit))
	 {
		if(hit.collider.tag != "MainCamera") //hit something else before the camera
		{
			//do something here
		}
	 }
	}

	void Start()
	{
		camTransform = transform;
		cam = Camera.main;
		//rigidbody = GetComponent<Rigidbody>();
		//toFollowLastPos = toFollow.position;
		//distanceToPlayer = Vector3.Distance(toFollowLastPos, transform.position);
	}

	void Update()
	{
		currentX += Input.GetAxis ("Mouse X");/*
		if (!isColliding)
		{
			Vector3 centerAtHeight = new Vector3(toFollow.position.x, transform.position.y, toFollow.position.z);
			Vector3 vectorCenterToPos = transform.position - centerAtHeight;
			Vector3 vectorToAddFromCenterToGetPos = vectorCenterToPos.normalized * distanceOnXZ;
			rigidbody.MovePosition(centerAtHeight + vectorToAddFromCenterToGetPos);
		}
		else
			rig.MovePosition(transform.position + deltaPos);


		toFollowLastPos = toFollow.position;

	*/}
    /*
	void OnTriggerStay(Collider col) {
		//check of the good layer
		isColliding = true;
	}
	void OnCollisionExit(Collision col){
		isColliding = false;
		//no need for check if the object is multi colliding, if it's the case, 'OnCollisionStay' will put it backto false
	}*/

	void LateUpdate()
	{
		Quaternion rotation = Quaternion.Euler (0, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);
	}
}