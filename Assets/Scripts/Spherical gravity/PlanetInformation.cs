﻿using UnityEngine;
using System.Collections;

public class PlanetInformation : MonoBehaviour {
	[SerializeField]private float gravityStrength;
	public float GravityStrength{get{return gravityStrength;}}

	//todo, gravity strength modifier based on player distance.
}