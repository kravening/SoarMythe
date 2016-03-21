using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

    [SerializeField]
    GameObject lastCheckpoint; // Last used checkpoint/chargepad. Used for going back.

    Transform tf;
    PowerContainer pc;

    bool touchingGround, touchingChargepad = false;

    public GameObject LastCheckpoint { // Used for saving last checkpoint.
        get {
            return lastCheckpoint;
        }
    }

	void Start () {
        // Getting them as soon as the class starts, because I will need them immediately after.
        tf = GetComponent<Transform>();
        pc = GetComponent<PowerContainer>();

	    if(lastCheckpoint != null)
            GoToCheckpoint(lastCheckpoint);

	}

    void OnCollisionEnter(Collision other) {
        bool isChargepad = other.gameObject.tag == Tags.CHARGEPAD;

        // Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
        // Plus I already needed this if statement to check if it's a chargepad, two bugs one stone.
        if (isChargepad && lastCheckpoint != other.gameObject) {
            lastCheckpoint = other.gameObject;
        }
    }

    /// <summary>
    /// Allows other classes to set the last checkpoint without going to it.
    /// </summary>
    /// <param name="checkpoint"></param>
    public void SetLastCheckpoint(GameObject checkpoint) {
        lastCheckpoint = checkpoint;
    }

    /// <summary>
    /// Returns the player back to last checkpoint.
    /// </summary>
    /// <returns>bool based on success of returning</returns>
    public bool ReturnToLastCheckpoint() {
        // In the unlikely event that someone forgot to set spawn.
        // Make sure everybody knows.
        if (lastCheckpoint != null) {
            tf.position = lastCheckpoint.transform.position;
            tf.position += lastCheckpoint.transform.up;
            return true;
        } else {
            print("Did you not set a spawnpoint? The player needs one if he returns!");
        }

        // If the if-statement returned false, return false to whoever called me.
        return false;
    }

    /// <summary>
    /// Checks if there is a checkpoint there and if there is, return to it.
    /// </summary>
    /// <param name="checkpointPosition"></param>
    /// <returns>If no checkpoint was found it returns false.</returns>
    public bool GoToCheckpoint(GameObject checkpoint) {
        // Check if given object is player spawn or chargepad/checkpoint.
        if (checkpoint.tag == Tags.CHARGEPAD || checkpoint.tag == Tags.SPAWN) {
            SetLastCheckpoint(checkpoint);
            pc.Power = pc.MaxPower;
            ReturnToLastCheckpoint();
            return true;
        }

        // If the gameobject did not meet the requirements return false to whoever called me.
        return false;
    }
}
