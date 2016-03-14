﻿using UnityEngine;
using System.Collections;

public class FindNearestPlanet : MonoBehaviour
{
    public float planetLerp = 1f;
    public float linkDistance = 10f;
    public float rotateCatchupSpeed = 1f;
    public float rotateSpeed = 0.1f;

    private Transform planet;
    private float lastPlanetChange;
    private Quaternion startQuat;
    private Vector3 startUp;

    private bool flying = false;
    private bool stickDown = true;
    private bool isTouching = false;

    void Update()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down); // update new down vector
        RaycastHit hit1; 
        RaycastHit hit2;
        isTouching = false;
        if (stickDown && Physics.Raycast(transform.position, dwn, out hit1, linkDistance))
        {
            linkToPlanet(hit1);
        }
        if (Physics.Raycast(transform.position, transform.up, out hit2, linkDistance))
        {
            if (!stickDown || hit2.distance < hit1.distance)
            {
                linkToPlanet(hit2);
            }
        }
        if (!isTouching && !flying)
        {
            transform.Rotate(rotateCatchupSpeed * Time.deltaTime, 0f, 0f);
        }
        isTouching = false;
    }
    void linkToPlanet(RaycastHit planet)
    {
        if (planet.transform.tag == Tags.PLANET)
        {
            isTouching = true;
            Transform lastPlanet = this.planet;
            this.planet = planet.transform;
            if (this.planet != lastPlanet)
            {
                gameObject.SendMessage("changePlanet", this.planet); // a function in the player to change target planet
                lastPlanetChange = Time.time;
                startQuat = transform.rotation;
                startUp = transform.up;
            }
            flying = false;
            float frac = (Time.time - lastPlanetChange);
            transform.rotation = Quaternion.FromToRotation(transform.up, planet.normal) * transform.rotation;
        }
    }
}