using UnityEngine;
using System.Collections;

public class SunPullTrigger : MonoBehaviour {

    SunPull sun;

    void Start() {
        sun = Object.FindObjectOfType<SunPull>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            sun.Pull = true;
        }
    }
}
