using UnityEngine;

public class PowerContainer : MonoBehaviour {

    [SerializeField][Range(0.001f,0.1f)][Tooltip("How fast the lerp charges the power.")]
    float chargeSpeed = 0.01f;

    [SerializeField][Tooltip("The amount of power that is and can be contained")] // The amount of jump power the player has, and the max he can have are both set here.
    float power, maxPower = 20;

    bool touchingChargepad;

    public float Power { // Mainly for the Power UI slider.
        get { return power; }
        set { power = value; }
    }

    public float MaxPower { // Mainly for saving/loading the max, and the power UI.
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
            power = Mathf.Lerp(power, maxPower, chargeSpeed);
            if (maxPower - power < 0.5f) {
                power = maxPower;
            }
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
