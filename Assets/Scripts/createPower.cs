using UnityEngine;
using System.Collections;

public class createPower : MonoBehaviour {

	public int getPower;

	// Use this for initialization
	void Start () {
		dummyPowerscript.dPower.AddPower (getPower);
	}
}
