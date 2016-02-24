using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	// This script is only for testing the powerups.
	// Will be removed once a proper movement comes up.
	bool touchingGround = false;
	bool touchingChargepad = false;

	Rigidbody rb;
	Transform tf;

	[SerializeField]
	GameObject particleGroundHit;

	[SerializeField]
	int power = 20;

	public int Power {
		get {
			return power;
		}
	}

	[SerializeField]
	int maxPower = 20;

	public int MaxPower {
		get {
			return maxPower;
		}
	}

	[SerializeField]
	Transform lastCheckpoint;

	public Transform LastCheckpoint {
		get {
			return lastCheckpoint;
		}
	}

	[SerializeField]
	LayerMask ground;

	void Start() {
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();

		print(GoToCheckpoint(lastCheckpoint.position));
	}

	void OnCollisionEnter(Collision other) {
		string tag = CollisionCheck(other);

		// Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
		// Plus I already needed this if statement to check if it's a chargepad, two bugs one stone.
		if (tag == Tags.CHARGEPAD && lastCheckpoint != other.gameObject) {
			lastCheckpoint = other.gameObject.transform;
		}

		//GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
		//newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
	}

	void OnCollisionStay(Collision other) {
		CollisionCheck(other);
	}

	void OnCollisionExit(Collision other) {
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
			return Tags.GROUND;
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
		if (lastCheckpoint != null) {
			tf.position = lastCheckpoint.transform.position;
			tf.position += lastCheckpoint.transform.up * 5;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Checks if there is a checkpoint there and if there is, return to it.
	/// </summary>
	/// <param name="checkpointPosition"></param>
	/// <returns>If no checkpoint was found it returns false.</returns>
	public bool GoToCheckpoint(Vector3 checkpointPosition) {
		Ray ray = new Ray(checkpointPosition, checkpointPosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 1);

		// In the event that it fails, I don't wanna try and check the tag
		// of a non existant GameObject. This'll do.
		if (hit.collider == null)
			return false;

		if (hit.collider.gameObject.tag == Tags.CHARGEPAD) {
			lastCheckpoint = hit.collider.gameObject.transform;
			ReturnToLastCheckpoint();
			return true;
		}

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
		if (!forward && !backward) {
			if (left) {
				tf.Rotate(new Vector3(0, -1));
				moveBy += movement / 1.25f;
			} else if (right) {
				tf.Rotate(new Vector3(0, 1));
				moveBy += movement / 1.25f;
			}

		// Else only edit rotation.
		} else {
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
			rb.AddForce(tf.up * 5, ForceMode.Impulse);
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