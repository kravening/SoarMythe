﻿using UnityEngine;
using System.Collections;

public class SunPull : MonoBehaviour {
    [SerializeField, Tooltip("How fast do I pull ToPull to ToPullTo?")]
    float speed = 0.1f;
    
    [SerializeField, Tooltip("What will I pull towards ToPullTo?")]
    Transform toPull;

    [SerializeField, Tooltip("What will I pull ToPull towards?")]
    Transform toPullTo;

    [SerializeField, Tooltip("Use rigidbody force or just move it there?")]
    bool byForce = true;

    public bool Pull = false;

    bool firstRun = true;

    Rigidbody rb;

    void Start() {
        rb = toPull.gameObject.GetComponent<Rigidbody>();
    }

	void Update () {
        if (Pull) {

            if (byForce) {
                rb.AddForce(-Vector3.MoveTowards(toPull.position, toPullTo.position, Time.deltaTime * speed) / 10);
            } else {
                toPull.position = Vector3.MoveTowards(toPull.position, toPullTo.position, Time.deltaTime * speed);
            }

            if (firstRun) {
                firstRun = false;
                rb.useGravity = false;
                rb.velocity *= 0.7f;
                toPull.gameObject.GetComponent<PlayerMovement>().IsAlive = false;
            }

        } else {
            if (!firstRun) {
                firstRun = true;

                rb.useGravity = true;

                toPull.gameObject.GetComponent<PlayerMovement>().IsAlive = true;
            }
        }
	}
}
