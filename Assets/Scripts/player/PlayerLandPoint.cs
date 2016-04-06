using UnityEngine;

public class PlayerLandPoint : MonoBehaviour {

    [SerializeField]
    Transform marker;

    [SerializeField]
    LayerMask ground;

    [SerializeField, Tooltip("From where I cast my ray")]
    Transform raycastPoint;

    [SerializeField, Tooltip("Layers to ignore")]
    LayerMask layerMask;

    PlayerMovement pm;

    void Start() {
        pm = GetComponent<PlayerMovement>();
    }

	void Update () {
        // Setup the rays position.
        Vector3 rayStart = raycastPoint.position;

        // Set up the ray.
        Ray ray = new Ray(rayStart, -transform.up);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, layerMask);

        GameObject other = hit.collider != null ? hit.collider.gameObject : null;

        Debug.DrawRay(rayStart, -transform.up * 10, Color.red, 0, false);

        if(other != null) {
            if (other.layer == 8) {
                if (!pm.TouchingGround) {
                    SetActive(true);

                    Vector3 positionToSetTo = hit.point;
                    positionToSetTo.y += 0.03f;

                    marker.position = positionToSetTo;
                } else {
                    SetActive(false);
                }
            } else {
                SetActive(false);
            }
        } else {
            SetActive(false);
        }
	}

    void SetActive(bool state) {
        marker.gameObject.SetActive(state);
    }
}
