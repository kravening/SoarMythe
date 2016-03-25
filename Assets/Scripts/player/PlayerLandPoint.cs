using UnityEngine;
using System.Collections;

public class PlayerLandPoint : MonoBehaviour {

    [SerializeField]
    Transform marker;

    [SerializeField]
    LayerMask ground;

	void Update () {
        Vector3 rayStart = transform.position;
        rayStart.y -= 0.96f;

        Ray ray = new Ray(rayStart, -transform.up);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        GameObject other = hit.collider.gameObject != null ? hit.collider.gameObject : null;

        #if UNITY_EDITOR
            Debug.DrawRay(rayStart, -transform.up, Color.red, 0, false);
        #endif

        if(other != null) {
            if (other.layer == 8) {
                marker.gameObject.SetActive(true);


                float plusY = other.transform.position.y + other.transform.lossyScale.y / 2 + 0.01f;

                marker.position = new Vector3(transform.position.x, plusY , transform.position.z);
            } else {
                marker.gameObject.SetActive(false);
            }
        } else {
            marker.gameObject.SetActive(false);
        }
	}
}
