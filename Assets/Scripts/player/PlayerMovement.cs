using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	bool touchingGround = false; // Am I touching ground? Used to tell the difference
								 // between a jump, and flight.
    [Header("Speed attributes:")]
    [SerializeField]
    float groundSpeed = 8;

    [SerializeField]
    float airSpeed = 2;

    [Tooltip("I highly suggest you keep this above 10, it just makes the game crash otherwise.")]
    [SerializeField] // Using this in the Move() function rather than the update so that speed can still be changed on the go.
    float speedDevider = 30; // Also using this to make the speed usable in just whole numbers while not making the player go insane speeds.

	Transform tf; // Used to do walking movement.
	Rigidbody rb; // Used to AddForce for the jump.
    CustomGravity cg; // Used to actually addforce for the jump.
    CheckpointController cc; // Used to set and goto last checkpoint.
    PowerContainer pc; // Used to retrieve the amount of power the player has and edit it.

    [Header("Miscellaneous attributes:")]
	[SerializeField] // When the player hits the ground, drop an instance of this.
	GameObject particleGroundHit;

	[SerializeField]
	LayerMask ground; // This is compared with the layer of whatever I am touching right now.
					  // So anything I can jump off has this as layer.

	void Start() {
		// Getting them as soon as the class starts, because I will need them immediately after.
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
        cg = GetComponent<CustomGravity>();
        cc = GetComponent<CheckpointController>();
        pc = GetComponent<PowerContainer>();
	}

	void OnCollisionEnter(Collision other) {
		// Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
		// Plus I already needed this if statement to check if it's a chargepad, two bugs one stone.
		if (other.gameObject.tag == Tags.CHARGEPAD) {
            cc.SetLastCheckpoint(other.gameObject);
            pc.TouchingChargepad = true;
		}

        if (other.gameObject.layer == ground) {
            GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
            newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
            newParticle.transform.rotation = tf.rotation;
        }
	}

	void OnCollisionExit(Collision other) {
		// Make sure we set whatever we stop touching to false, so you can't charge or jump from thin air.
		if (other.gameObject.layer == ground) {
			touchingGround = false;
		}

		if (other.gameObject.tag == Tags.CHARGEPAD) {
            pc.TouchingChargepad = false;
		}
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
        Vector3 movement = touchingGround ? tf.forward * (groundSpeed / speedDevider) : tf.forward * (airSpeed / speedDevider);

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
            //Can uncomment, not like it will work anyway.
			rb.AddForce(tf.up * 10000, ForceMode.Impulse);
            //cg.AddForce(tf.up * 100000);

            // This turns out to be the way CustomGravity did the jump.
            // It worked but it's odd.
            //cg.acceleration += tf.up * 5;
            //cg.speed.y += cg.jumpReact;
		}
		// The flight, only works if the player has enough power to remove.
		else if (jump && pc.Power >= 10) {
			pc.Power -= 10;
			rb.AddForce(tf.up * 5, ForceMode.Force);
		}
		// If the power is less, let it glide.
		// Glide remains true while the space bar is down. Unlike the jump.
        else if (glide && pc.Power < 10) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
		}   
	}
}