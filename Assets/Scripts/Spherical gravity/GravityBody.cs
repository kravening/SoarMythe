using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	GravityAttractor currentGravityObject;
	Rigidbody rigidbody;
	
	void Awake () {
		currentGravityObject = GameObject.FindGameObjectWithTag("StartingGravityPoint").GetComponent<GravityAttractor>();
		rigidbody = GetComponent<Rigidbody> ();

		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () {
		currentGravityObject.Attract(rigidbody);
	}
}