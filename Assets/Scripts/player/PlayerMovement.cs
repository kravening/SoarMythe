using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	bool touchingGround = false; // Am I touching ground? Used to tell the difference
								 // between a jump, and flight.
	bool touchingChargepad = false; // Used to let the charging happen.

	Transform tf; // Used to do walking movement.
	Rigidbody rb; // Used to AddForce for the jump;

	[SerializeField] // When the player hits the ground, drop an instance of this.
	GameObject particleGroundHit;

	[SerializeField] // The amount of jump power the player has.
	int power = 20;

	public int Power { // Mainly for the Power UI slider.
		get {
			return power;
		}
	}

	[SerializeField] // Used to have a limit on how much the player can use his flight.
	int maxPower = 20;

	public int MaxPower { // Mainly for saving/loading the max, and the power UI.
		get {
			return maxPower;
		}
		set {
			maxPower = value;
		}
	}

	[SerializeField]
	GameObject lastCheckpoint; // Last used checkpoint/chargepad. Used for going back.

	public GameObject LastCheckpoint { // Used for saving last checkpoint.
		get {
			return lastCheckpoint;
		}
	}

	[SerializeField]
	LayerMask ground; // This is compared with the layer of whatever I am touching right now.
					  // So anything I can jump off has this as layer.

	void Start() {
		// Getting them as soon as the class starts, because I will need them immediately after.
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();

		print(GoToCheckpoint(lastCheckpoint));
	}

	void OnCollisionEnter(Collision other) {
		string tag = CollisionCheck(other);

		// Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
		// Plus I already needed this if statement to check if it's a chargepad, two bugs one stone.
		if (tag == Tags.CHARGEPAD && lastCheckpoint != other.gameObject) {
			lastCheckpoint = other.gameObject;
		}

		//GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
		//newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
	}

	void OnCollisionExit(Collision other) {
		// Make sure we set whatever we stop touching to false, so you can't charge or jump from thin air.
		if (other.gameObject.layer == ground) {
			touchingGround = false;
		}

		if (other.gameObject.tag == Tags.CHARGEPAD) {
			touchingChargepad = false;
		}
	}

	// Earlier this was nessecary because two functions would repeat.
	// But to do checkpoints it's slighty different for OnCollisionEnter
	// Because of that it returns the tag name.
	/// <summary>
	/// Check's tag on other, edit's variables accordingly and returns the tag.
	/// </summary>
	/// <param name="other">Collision, retrieved by event function.</param>
	/// <returns>The tag of other</returns>
	string CollisionCheck(Collision other) {
		if (other.gameObject.layer == ground) {
			touchingGround = true;
		}
		else if (other.gameObject.tag == Tags.CHARGEPAD) {
			touchingChargepad = true;
			return Tags.CHARGEPAD;
		}
		return other.gameObject.tag;
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
			tf.position += lastCheckpoint.transform.up * 5;
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
	public bool GoToCheckpoint(GameObject checkpointPosition) {
		// Check if given object is player spawn or chargepad/checkpoint.
		if (checkpointPosition.tag == Tags.CHARGEPAD || checkpointPosition.tag == Tags.SPAWN) {
			lastCheckpoint = checkpointPosition;
			power = maxPower;
			ReturnToLastCheckpoint();
			return true;
		}

		// If the gameobject did not meet the requirements return false to whoever called me.
		return false;
	}

	/// <summary>
	/// This method should only be called by input classes.
	/// So the defaults shouldn't matter.
	/// But they're there to be on the safe side.
	/// </summary>
	/// <param name="forward">Moving forward?</param>
	/// <param name="backward">Moving backwards?</param>
	/// <param name="left">Moving left?</param>
	/// <param name="right">Moving right?</param>
	/// <param name="jump">Should I jump?</param>
	/// <param name="glide">Am I gliding?</param>
	public void Move(bool forward = false, bool backward = false, bool left = false, bool right = false, bool jump = false, bool glide = false) {
		// This is probably really intensive, considering it happens during Update();
		// Although tf.forward is not constant so I really need to recreate this every time.
		Vector3 movement = touchingGround ? tf.forward / 10 : tf.forward / 25;

		// I need this so that I can edit it if the player moves with left or right.
		Vector3 moveBy = new Vector3();

		// Just add movement.
		if (forward) {
			moveBy += movement;
		}
		else if (backward) {
			moveBy -= movement;
		}

		// If not adding movement from forward or backward before this.
		// Otherwise you get double speed, while nice it's not the wanted
		// result.
		if (!forward && !backward) {
			if (left) {
				tf.Rotate(new Vector3(0, -1));
				moveBy += movement / 1.25f;
			} else if (right) {
				tf.Rotate(new Vector3(0, 1));
				moveBy += movement / 1.25f;
			}
		} else { // Else only edit rotation.
			if (left) {
				tf.Rotate(new Vector3(0, -1));
			} else if (right) {
				tf.Rotate(new Vector3(0, 1));
			}
		}

		// Edit movement if I did use forward or backward while also doing left or right.
		if (left || right) {
			if (forward || backward) {
				moveBy /= 1.25f;
			}
		}

		// Then finally add movement.
		tf.position += moveBy;

		// This would only trigger the frame the space bar was pressed. And aside from that only
		// if the player was touching the ground.
		if (jump && touchingGround) {
			rb.AddForce(tf.up * 10, ForceMode.Impulse);
		}
		// The flight, only works if the player has enough power to remove.
		else if (jump && power >= 10) {
			power -= 10;
			rb.AddForce(tf.up * 5, ForceMode.Force);
		}
		// If the power is less, let it glide.
		// Glide remains true while the space bar is down. Unlike the jump.
		else if (glide && power < 10) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
		}

		// If I am standing on a chargepad, and not jumping.
		// Charge up!
		if (!jump && touchingChargepad && power <= maxPower) {
			power += 5;
		}

		// Don't wanna have more than the max power.
		if (power > maxPower) {
			power = maxPower;
		}
	}

	/// <summary>
	/// Add to the max power.
	/// If this function is called without giving a toAdd, it defaults to 10;
	/// </summary>
	/// <param name="toAdd">Raise the max by toAdd.</param>
	public void PowerUp(int toAdd = 10) {
		maxPower += toAdd;
	}
}