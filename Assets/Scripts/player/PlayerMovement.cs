using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
    [SerializeField]
	bool touchingGround = false; // Am I touching ground? Used to tell the difference
								 // between a jump, and flight.

    public bool TouchingGround {
        get { return touchingGround; }
    }

    [Header("Movement attributes:")]

    // Used to move according to the camera.
    [SerializeField][Tooltip("The block that follows the camera. The block should contain a PlayerMovementCameraPosition class.")]
    Transform CameraPosition;

     // Speed on the ground.
    [SerializeField][Tooltip("How the fast the player will move when on the ground.")]
    float groundSpeed = 8;

    // Speed in the air.
    [SerializeField][Tooltip("How the fast the player will move when in the air.")]
    float airSpeed = 2;

    // How high can we jump.
    [SerializeField][Tooltip("How high the player can jump.")]
    float jumpHeight = 8;

    // This is the fly boost and is used when the player has enough power and is in the air.
    /*[SerializeField][Tooltip("How much height will I be given when I fly.")]
    float flightHeight = 5;*/

    [SerializeField][Tooltip("I highly suggest you keep this above 10, it just makes the game crash otherwise.")]
                             // Using this in the Move() function rather than the update so that speed can still be changed on the go.
    float speedDivider = 30; // Also using this to make the speed usable in just whole numbers while not making the player go insane speeds.

    [SerializeField][Tooltip("I highly suggest you keep this above 10, it just makes the game crash otherwise.")]
    float jumpDivider = 30; // Also using this to make the jumpheight usable in just whole numbers while not making the player jump insanely high.

	Transform tf; // Used to do walking movement.
	Rigidbody rb; // Used to AddForce for the jump.
    CustomGravity cg; // Used to actually addforce for the jump.
    CheckpointController cc; // Used to set and goto last checkpoint.
    PowerContainer pc; // Used to retrieve the amount of power the player has and edit it.

    [Header("Miscellaneous attributes:")]

    // When the player hits the ground, drop an instance of this.
	[SerializeField][Tooltip("The particle that will be instantiated when the player lands on the ground.")]
	GameObject particleGroundHit;

	[SerializeField][Tooltip("The layer Ground should be in this to detect if the player is touching the ground.")]
	LayerMask groundLayer; // This is compared with the layer of whatever I am touching right now.
					       // So anything I can jump off has this as layer.

	void Start() {
		// Getting them as soon as the class starts, because I will need them immediately after.
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
        cc = GetComponent<CheckpointController>();
        pc = GetComponent<PowerContainer>();

        // The artists tend to have issues throwing a player into their game, giving these errors if the player is lacking something will help
        // them on their way.
        if (cc == null) {
            Debug.LogError("The player gameobject does not contain a CheckpointController class!");
        }

        if (pc == null) {
            Debug.LogError("The player gameobject does not contain a PowerContainer class!");
        }

        if (GetComponent<KeyboardInput>() == null || GetComponent<Xbox360Wired_InputController>() == null) {
            Debug.LogError("The player gameobject is lacking an input class!");
        }
	}

	void OnTriggerEnter(Collider other) {
		// Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
		// Then if it's a checkpoint, check if it's not last and if it ain't make it the last.
		if (other.gameObject.tag == Tags.CHARGEPAD) {
            pc.TouchingChargepad = true;
            if(cc.LastCheckpoint != other.gameObject)
                cc.SetLastCheckpoint(other.gameObject);
		}

        if (other.gameObject.layer == 8) {
            GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
            newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
            newParticle.transform.rotation = tf.rotation;
            touchingGround = true;
        }
	}

    void OnTriggerExit(Collider other) {
		// Make sure we set whatever we stop touching to false, so you can't charge or jump from thin air.
        if (other.gameObject.layer == 8) {
			touchingGround = false;
		}

		if (other.gameObject.tag == Tags.CHARGEPAD) {
            pc.TouchingChargepad = false;
		}
	}

    // Just made these two cause it was a lot faster and smaller then calling the full function again and again.
    private Quaternion QuatLookRot(Vector3 i) {
        return Quaternion.LookRotation(i);
    }

    private Quaternion QuatLerp(Quaternion to, float time) {
        return Quaternion.Lerp(transform.rotation, to, time);
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

        Vector3 forwardMovement;
        Vector3 rightMovement = new Vector3();

        if (CameraPosition != null) {
            // This is probably really intensive, considering it happens during Update();
            // Although tf.forward is not constant so I really need to recreate this every time.
            forwardMovement = touchingGround ? CameraPosition.forward * (groundSpeed / speedDivider) : CameraPosition.forward * (airSpeed / speedDivider);
            rightMovement = touchingGround ? CameraPosition.right * (groundSpeed / speedDivider) : CameraPosition.right * (airSpeed / speedDivider);
        } else {
            forwardMovement = touchingGround ? tf.forward * (groundSpeed / speedDivider) : tf.forward * (airSpeed / speedDivider);
        }

		// I need this so that I can edit it if the player moves with left or right.
		Vector3 moveBy = new Vector3();

		// Just add movement.
        // If no cameraPosition was added, I'll move make it move based
        // on the player instead.
        // If it is added, move based on the camera.
        if (CameraPosition != null) {

            // Move the player facing away from the camera.

            float DelTime = Time.deltaTime * 7.5f;
            Vector3 CamForward = CameraPosition.forward;
            Vector3 CamRight = CameraPosition.right;

            // Make the player face the direction he is walking.
            // This makes it possible to go upto 8 directions. Which is also how much he can walk in the actual movement.
            if(!left && !right) {
                if (forward)
                    transform.rotation = QuatLerp(CameraPosition.rotation, DelTime);
                else if (backward)
                    transform.rotation = QuatLerp(QuatLookRot(-CamForward), DelTime);
            } else if (!forward && !backward) {
                if (left)
                    transform.rotation = QuatLerp(QuatLookRot(-CamRight), DelTime);
                else if (right)
                    transform.rotation = QuatLerp(QuatLookRot(CamRight), DelTime);
            } else {
                if (forward) {
                    if (left)
                        transform.rotation = QuatLerp(QuatLookRot(-CamRight + CamForward), DelTime);
                    else if (right)
                        transform.rotation = QuatLerp(QuatLookRot(CamRight + CamForward), DelTime);
                } else if (backward) {
                    if (left)
                        transform.rotation = QuatLerp(QuatLookRot(-CamRight + -CamForward), DelTime);
                    else if (right)
                        transform.rotation = QuatLerp(QuatLookRot(CamRight + -CamForward), DelTime);
                }
            }

            // After we made him face the right way, actually go the right way.
            // If I press forward or backward, apply movement accordingly.
            if (forward) {
                moveBy += forwardMovement;
            } else if (backward) {
                moveBy += -forwardMovement;
            }

            // If I'm just walking left or right, do that.
            if (!forward && !backward) {
                if (left) {
                    moveBy += -rightMovement / 1.25f;
                } else if (right) {
                    moveBy += rightMovement / 1.25f;
                }
            }

            // If I was moving forwards or backwards(cannot be both) apply left or right movement aswell.
            if (forward || backward) {
                if (left) {
                    moveBy += -rightMovement / 1.25f;
                } else if (right) {
                    moveBy += rightMovement / 1.25f;
                }
            }
        } else {
            // We didn't get a CameraPos GameObject, so we'll just walk the old fashioned way.
            // Using the forward of the player rather than the camera's.

            // If not going left or right, move forward or backward.
            if (!left && !right) {
                if (forward) {
                    moveBy += forwardMovement;
                } else if (backward) {
                    moveBy += -forwardMovement;
                }
            }

            // If only going left or right, rotate and move forward.
            if (!forward && !backward) {
                if (left) {
                    tf.Rotate(new Vector3(0, -1));
                    moveBy += forwardMovement / 1.25f;
                } else if (right) {
                    tf.Rotate(new Vector3(0, 1));
                    moveBy += forwardMovement / 1.25f;
                }
            } else { // Else only edit rotation.
                if (left) {
                    tf.Rotate(new Vector3(0, -1));
                } else if (right) {
                    tf.Rotate(new Vector3(0, 1));
                }
            }
        }

		// Then finally the add movement I made above.
		tf.position += moveBy;

        
        // Then for the finale movement ability, the jump.
        // This isn't dependant on the CameraPos because it's just up.
        // So doing it last is for the best.
		// This would only trigger the frame the space bar was pressed. And aside from that only
		// if the player was touching the ground.
		if (jump && touchingGround) {
            rb.AddForce(tf.up * (jumpHeight / jumpDivider), ForceMode.Impulse);
        } else if (jump && pc.Power > 0.5f) {
            // Glide remains true while the the jump button is down. Unlike the actual jump
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
            pc.Power -= 0.5f;
        }
        
        /*if (jump && pc.Power >= 10) {
            // The flight, only works if the player has enough power to remove.
			pc.Power -= 10;
            rb.AddForce(tf.up * (flightHeight / jumpDivider), ForceMode.Impulse);
            Debug.Log("fly");
		}*/  
	}
}