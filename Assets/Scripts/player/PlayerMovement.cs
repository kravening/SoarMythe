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
	GameObject lastCheckpoint;

	public GameObject LastCheckpoint {
		get {
			return lastCheckpoint;
		}
	}

	[SerializeField]
	RaycastHit hit;

	[SerializeField]
	LayerMask ground;

	void Start() {
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
	}

	void FixedUpdate() {
		Debug.DrawLine(tf.position, -tf.up, Color.red);

		Physics.Raycast(tf.position, -tf.up, 1);
		print(hit.transform);
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
	// Because it returns the tag name.
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
			tf.position += tf.up * 5;
			return true;
		}

		return false;
	}

	public void Move(bool forward, bool backward, bool left, bool right, bool jump, bool glide) {
		// This is probably really intensive, considering it happens during Update();
		Vector3 movement = touchingGround ? tf.forward / 10 : tf.forward / 25;


		if (forward) {
			tf.position += movement;
		}
		else if (backward) {
			tf.position -= movement;
		}

		if (!forward && !backward) {
			if (left) {
				tf.Rotate(new Vector3(0, -1));
			} else if (right) {
				tf.Rotate(new Vector3(0, 1));
			}
		} else {

		}

		if (jump && touchingGround) {
			rb.AddForce(tf.up * 10, ForceMode.Impulse);
		}
		else if (jump && power >= 10) {
			power -= 10;
			rb.AddForce(tf.up * 5, ForceMode.Impulse);
		}
		else if (glide && power < 10) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
		}

		if (!jump && touchingChargepad && power <= maxPower) {
			power += 5;
		}

		if (power > maxPower) {
			power = maxPower;
		}
	}

	public void PowerUp(int i = 10) {
		maxPower += i;
	}
}