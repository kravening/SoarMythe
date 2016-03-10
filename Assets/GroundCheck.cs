using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
    private bool grounded = false;
    public bool getGroundedState { get { return grounded; } }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.SURFACE)
        {
            grounded = true;
        }
    }
}
