using UnityEngine;
using System.Collections;

public class dummyPowerscript : MonoBehaviour {

	public static dummyPowerscript dPower;
	public int Power = 0;

	public void AddPower(int totalPower)
	{
		Power += totalPower;
	}
}
