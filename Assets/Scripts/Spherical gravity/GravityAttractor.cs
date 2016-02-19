using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {
	
	public float gravity = -9.81f; // Editable for gliding
	[SerializeField]int gravitationalPriority = 0; // Higher nubmer higher priority
	
	public void Attract(Rigidbody obj) {
		Vector3 gravityUp = (obj.position - transform.position).normalized;
		Vector3 localUp = obj.transform.up;
		
		// Applies downwards gravity to the object
		obj.AddForce(gravityUp * gravity);
		// Rotation alignment of the obj to the center (change this to a normal rotation system)
		obj.rotation = Quaternion.FromToRotation(localUp,gravityUp) * obj.rotation;
	}  
}
