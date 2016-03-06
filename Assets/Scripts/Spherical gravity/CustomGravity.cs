﻿using UnityEngine;
using System.Collections;

public class CustomGravity : MonoBehaviour {
	public float mass = 0.3f;
	public float moveAcceleration = 0.3f;
	public float maxMoveSpeed = 2.0f;
	public float slowdownTime = 0.3f;

	private Vector3 acceleration = new Vector3(0f, 0f, 0f);
	private Vector3 speed = new Vector3 (0f, 0f, 0f);
	private float moveSpeed = 0f;
	private Vector3 lastPosition;
	private Vector3 lastSpeed;
	private Vector3 lastAcceleration;

	private bool floored = false;

	private PlanetInformation planetInfo;

	//debug
	private Vector3 lastDiff;
	// Use this for initialization
	void Start () {
		lastDiff = new Vector3(0, 0, 0);
	}
		
	void ProcessGravity () {
		if (!floored) {
			acceleration += new Vector3(0f, -mass * ((planetInfo != null) ? planetInfo.GravityStrength : 1), 0f);
		}

		speed += Time.deltaTime * acceleration;

		Vector3 deltaSpeed = speed * Time.deltaTime;
		transform.position += transform.right * deltaSpeed.x;
		transform.position += transform.up * deltaSpeed.y;
		transform.position += transform.forward * deltaSpeed.z;
	}

	void Update () {
		lastPosition = transform.position;
		lastAcceleration = acceleration;
		lastSpeed = speed;

		ProcessGravity();
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Planet") {
			acceleration = Vector3.zero;
			speed = Vector3.zero;
			floored = true;
			planetInfo = other.GetComponent<PlanetInformation>();
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Planet") {
			floored = false;
		}
	}

	public void ChangePlanet (Transform newGravityObject) {
		acceleration = Vector3.zero;
		speed = Vector3.zero;
		ProcessGravity ();
	}

	
}
