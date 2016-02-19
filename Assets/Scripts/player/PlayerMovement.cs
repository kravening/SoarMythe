using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    // This script is only for testing the powerups.
    // Will be removed once a proper movement comes up.
    [SerializeField]
    bool touchingGround = false;

    Rigidbody rb;
    Transform tf;

    [SerializeField]
    GameObject particleGroundHit;

    [SerializeField]
    int power = 20;

    [SerializeField]
    int maxPower = 20;

    [SerializeField]
    RaycastHit hit;

    void Start() {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == Tags.GROUND) {
            touchingGround = true;
            GameObject newParticle = Instantiate<GameObject>(particleGroundHit);
            newParticle.transform.position = new Vector3(tf.position.x, tf.position.y - tf.position.y * 0.9f, tf.position.z);
        }
    }

    void Update() {
        Debug.DrawLine(tf.position, new Vector3(tf.position.x, tf.position.y - 1, tf.position.z), Color.red, 0);

        Physics.Raycast(tf.position, -tf.up, 1);
        print(hit.transform);
    }

    void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == Tags.GROUND) {
            touchingGround = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == Tags.GROUND) {
            touchingGround = false;
        }
    }

    public void Move(bool forward, bool backward, bool left, bool right, bool jump, bool glide) {
        Vector3 movement = touchingGround ? tf.forward /10 : tf.forward / 25;

        if (forward) {
            tf.position += movement;
        } else if (backward) {
            tf.position -= movement;
        }

        if (left) {
            tf.Rotate(new Vector3(0, -1));
        } else if (right) {
            tf.Rotate(new Vector3(0, 1));
        }

        if (jump && touchingGround) {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        } else if (jump && power >= 10) {
            power -= 10;
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        } else if (glide && power < 10) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.8f, rb.velocity.z);
        }

        if (!jump && touchingGround && power <= maxPower) {
            power += 5;
        }

        if (power > maxPower) {
            power = maxPower;
        }
    }
}
