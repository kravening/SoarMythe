using UnityEngine;
using System.Collections;

public class SunPowerDrain : MonoBehaviour {

    [SerializeField]
    [Tooltip("Distance * drain = damage to max power")]
    float drain, toLose;

    void OnTriggerStay(Collider other) {
        if (other.tag == Tags.PLAYER) {
            PowerContainer pc = other.GetComponent<PowerContainer>();
            if (pc.MaxPower > 0)
                toLose = drain / Vector3.Distance(transform.position, other.transform.position);

            pc.PowerUp(-toLose);
        }
    }
}