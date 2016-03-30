using UnityEngine;
using System.Collections;

public class PlayerLandPoint : MonoBehaviour {

    [SerializeField]
    Transform marker;

    [SerializeField]
    LayerMask ground;

    PlayerMovement pm;

    void Start() {
        pm = GetComponent<PlayerMovement>();
    }

	void Update () {
        // Setup the rays position.
        Vector3 rayStart = transform.position;
        rayStart.y -= transform.lossyScale.y / 1.2f;

        // Set up the ray.
        Ray ray = new Ray(rayStart, -transform.up);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        GameObject other = hit.collider != null ? hit.collider.gameObject : null;

        #if UNITY_EDITOR // Just to be on the safe side.
            Debug.DrawRay(rayStart, -transform.up * 10, Color.red, 0, false);
        #endif

        if(other != null) {
            if (other.layer == 8) {
                if (!pm.TouchingGround) {
                    SetMarkerActive(true);

                    Vector3 positionToSetTo = hit.point;
                    positionToSetTo.y += 0.03f;

                    marker.position = positionToSetTo;
                } else {
                    SetMarkerActive(false);
                }
            } else {
                SetMarkerActive(false);
            }
        } else {
            SetMarkerActive(false);
        }
	}

    void SetMarkerActive(bool state) {
        marker.gameObject.SetActive(state);
    }
}
