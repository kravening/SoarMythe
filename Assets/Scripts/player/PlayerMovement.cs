using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    bool touchingGround = false; // Am I touching ground? Used to tell the difference
                                 // between a jump, and flight.

    public bool TouchingGround {
        get { return touchingGround; }
    }

    [SerializeField]
    bool isAlive = true; // Am I alive? Used to make the player unable to move.

    public bool IsAlive {
        get { return isAlive; }
        set { isAlive = value; }
    }

    Transform tf; // Used to do walking movement.
    Rigidbody rb; // Used to AddForce for the jump.
    CheckpointController cc; // Used to set and goto last checkpoint.
    PowerContainer pc; // Used to retrieve the amount of power the player has and edit it.
    AnimationController ac; // To activate animations with.

    // Make it once and just keep editing it.
    Vector3 vel;

    // Used to move according to the camera.
    /*[SerializeField, Tooltip("The block that follows the camera. The block should contain a PlayerMovementCameraPosition class.")]*/
    Transform CameraPosition;

    [Header("Movement attributes:")]

    // I don't want the player to be able to just shoot himself into space with stacking force of the jump.
    [SerializeField, Tooltip("Not to have the player fly into space when you spam jump.")]
    float maxVelocity = 10;

    // Speed on the ground.
    [SerializeField, Tooltip("How the fast the player will move when on the ground.")]
    float groundSpeed = 8;

    // Speed in the air.
    [SerializeField, Tooltip("How the fast the player will move when in the air.")]
    float airSpeed = 2;

    // How high can we jump.
    [SerializeField, Tooltip("How high the player can jump.")]
    float jumpHeight = 8;

    // This is the fly boost and is used when the player has enough power and is in the air.
    [SerializeField, Tooltip("How much height will I be given when I fly.")]
    float flightHeight = 5;

    [SerializeField, Tooltip("I highly suggest you keep this above 10, it just makes the game crash otherwise.")]
    // Using this in the Move() function rather than the update so that speed can still be changed on the go.
    float speedDivider = 30; // Also using this to make the speed usable in just whole numbers while not making the player go insane speeds.

    [SerializeField, Tooltip("I highly suggest you keep this above 10, it just makes the game crash otherwise.")]
    float jumpDivider = 30; // Also using this to make the jumpheight usable in just whole numbers while not making the player jump insanely high.

    // This is really not a fully working thing.
    // I never bothered to fix it. But it's there.
    [SerializeField, Tooltip("Will I turn when I go left or right?")]
    bool smoothTurning = false;
    
    // For smooth turning, doesn't work that well anyway.
    [SerializeField, Tooltip("Speed at which I turn.")]
    float turningSpeed = 1;

    [Header("Power consumption")]

    // The amount of power a jump boost takes.
    [SerializeField, Tooltip("This happens only when the player jumps in the air.")]
    float jumpConsumption = 10;

    // The amount of power gliding takes each frame.
    [SerializeField, Tooltip("This is negated each frame. So don't have it too high.")]
    float glideConsumption = 0.5f;

    [Header("Miscellaneous attributes:")]

    // When the player hits the ground, drop an instance of this.
    [SerializeField, Tooltip("The particle that will be instantiated when the player lands on the ground.")]
    GameObject particleGroundHit;

    [SerializeField, Tooltip("The layer Ground should be in this to detect if the player is touching the ground.")]
    LayerMask groundLayer; // This is compared with the layer of whatever I am touching right now.
    // So anything I can jump off has this as layer.

    void Start() {
        // Getting them as soon as the class starts, because I will need them immediately after.
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        cc = GetComponent<CheckpointController>();
        pc = GetComponent<PowerContainer>();
        ac = GetComponent<AnimationController>();

        vel = new Vector3();

        CameraPosition = Object.FindObjectOfType<PlayerMovementCameraPosition>().gameObject.GetComponent<Transform>();

        // The artists tend to have issues throwing a player into their game, giving these errors if the player is lacking something will help
        // them on their way.
        if (cc == null) {
            Debug.LogError("The player gameobject does not contain a CheckpointController class!");
        }

        if (pc == null) {
            Debug.LogError("The player gameobject does not contain a PowerContainer class!");
        }

        if (GetComponent<KeyboardInput>() == null || GetComponent<XboxInputGame>() == null) { // Something is haunted about this line...
            Debug.LogError("The player gameobject is lacking an input class!");
        }
    }

    // I need to check enter and stay due to a small flaw that turns the touchingground off if you stop touching a wall.
    void OnTriggerEnter(Collider other) {
        CollisionChecker(other, false);
    }

    void OnTriggerStay(Collider other) {
        CollisionChecker(other);
    }

    void CollisionChecker(Collider other, bool spawnParticles = false) {
        // Changing last checkpoint to the last checkpoint would be pointless, and just extra resources we need.
        // Then if it's a checkpoint, check if it's not last and if it ain't make it the last.
        if (other.gameObject.tag == Tags.CHARGEPAD) {
            pc.TouchingChargepad = true;
            if (cc.LastCheckpoint != other.gameObject)
                cc.SetLastCheckpoint(other.gameObject);
        }

        if (other.gameObject.layer == 8) {
            touchingGround = true;
            if (spawnParticles) {
                GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
                newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
                newParticle.transform.rotation = tf.rotation;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        // Make sure we set whatever we stop touching to false, so you can't charge or jump from thin air.
        if (other.gameObject.layer == 8) {
            touchingGround = false;
        }

        if (other.gameObject.tag == Tags.CHARGEPAD || other.gameObject.tag == Tags.SPAWN) {
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
        Vector3 forwardMovement; // This is needed in all cases.

        // These two are given empty vectors, they are not always needed but will throw errors if I leave them empty.
        Vector3 sideForwardMovement = new Vector3();
        Vector3 rightMovement = new Vector3();

        if (isAlive) {
            if (CameraPosition != null) {
                // This is probably really intensive, considering it happens during FixedUpdate();
                // Although tf.forward is not constant so I really need to recreate this every time.
                forwardMovement = touchingGround ? CameraPosition.forward * (groundSpeed / speedDivider) : CameraPosition.forward * (airSpeed / speedDivider);
                sideForwardMovement = touchingGround ? tf.forward * (groundSpeed / speedDivider) : CameraPosition.forward * (airSpeed / speedDivider);
                rightMovement = touchingGround ? CameraPosition.right * (groundSpeed / speedDivider) : CameraPosition.right * (airSpeed / speedDivider);
            } else {
                forwardMovement = touchingGround ? tf.forward * (groundSpeed / speedDivider) : tf.forward * (airSpeed / speedDivider);
            }
        } else {
            forwardMovement = new Vector3();

            rightMovement = new Vector3();
            sideForwardMovement = new Vector3();

            touchingGround = false;
        }

        // I need this so that I can edit it if the player moves with left or right.
        Vector3 moveBy = new Vector3();

        // Just add movement.
        // If no cameraPosition was added, I'll make it move
        // based on the player instead.
        // If it is added, move according to the camera.
        if (CameraPosition != null) {

            // Move the player facing away from the camera.

            float DelTime = Time.deltaTime * 7.5f;
            Vector3 CamForward = CameraPosition.forward;
            Vector3 CamRight = CameraPosition.right;

            Quaternion fullRotation = QuatLerp(QuatLookRot(-CamRight), 1);

            // Make the player face the direction he is walking.
            // This makes it possible to go upto 8 directions. Which is also how much he can walk in the actual movement.
            if (isAlive) {
                if (!left && !right) {
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
            } else {
                Transform sun = GameObject.Find("Sun").transform;

                transform.rotation = Quaternion.RotateTowards(tf.rotation, sun.rotation, 1);
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

                // If either going left or right and smoothturning is on apply forward movement.
                if (smoothTurning) {
                    if(left) {
                        if(Quaternion.Angle(fullRotation, tf.rotation) > 10) {
                            moveBy += forwardMovement * turningSpeed;
                        }
                    } else if(right) {   
                        if(Quaternion.Angle(fullRotation, tf.rotation) < 170) {
                            moveBy += forwardMovement * turningSpeed;
                        }
                    }
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
        if(isAlive)
            tf.position += moveBy;


        // Then for the finale movement ability, the jump.
        // This isn't dependant on the CameraPos because it's just up.
        // So doing it last is for the best.
        // This would only trigger the frame the space bar was pressed. And aside from that only
        // if the player was touching the ground.
        if (jump && touchingGround) {
            rb.AddForce(tf.up * (jumpHeight / jumpDivider), ForceMode.Impulse);
        } else if (jump && pc.Power >= jumpConsumption) {
            // The flight, only works if the player has enough power to remove.
            pc.Power -= jumpConsumption;
            rb.AddForce(tf.up * (flightHeight / jumpDivider), ForceMode.Impulse);
        }
		
		if (glide && pc.Power > glideConsumption && !touchingGround) {
            // Glide remains true while the the jump button is down. Unlike the actual jump
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
            pc.Power -= glideConsumption;
        }

        if(jump && !glide) {
            if (!touchingGround) {
                Vector3 vel = rb.velocity;

                if (vel.y > maxVelocity) {
                    vel.y = maxVelocity;
                }

                rb.velocity = vel;
            }
        }

        // If any of these is true, it will send true.
        // If they're all false it will send false.
        bool moving = left || right || forward || backward;

        EditAnimations(moving, jump, glide);
    }

    void EditAnimations(bool moving, bool jump, bool glide) {
        ac.IsRunning = moving;
        ac.HasJumped = jump;
        ac.TouchingGround = touchingGround;
        ac.IsGliding = glide && pc.Power > 0;
        ac.Charging = pc.Power < pc.MaxPower && pc.TouchingChargepad ? true : false;
        //print(moving + " || " + jump + " || " + touchingGround + " || " + glide);
    }
}