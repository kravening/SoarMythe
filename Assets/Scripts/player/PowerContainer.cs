using UnityEngine;
using System.Collections;

public class PowerContainer : MonoBehaviour {

    [SerializeField] // The amount of jump power the player has, and the max he can have are both set here.
    int power, maxPower = 20;

    bool touchingChargepad;

    public int Power { // Mainly for the Power UI slider.
        get { return power; }
        set { power = value; }
    }

    public int MaxPower { // Mainly for saving/loading the max, and the power UI.
        get { return maxPower;  }
    }

    public bool TouchingChargepad { //
        get { return touchingChargepad; }
        set { touchingChargepad = value; }
    }

	void Update () {


        // Don't wanna have more than the max power.
        if (power > maxPower) {
            power = maxPower;
        }

        // If I am standing on a chargepad, and not jumping.
        // Charge up!
        if (touchingChargepad && power < maxPower) {
            power += 5;
        }
	}

    /// <summary>
    /// Add to the max power.
    /// If this function is called without giving a toAdd, it defaults to 10.
    /// However this could also be used to remove maxpower if need be.
    /// </summary>
    /// <param name="toAdd">Raise the max by toAdd.</param>
    public void PowerUp(int toAdd = 10) {
        maxPower += toAdd;
    }
}
