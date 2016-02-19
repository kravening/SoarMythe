using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
    [SerializeField]
    GameObject particleOnDeath;

    void OnTriggerEnter(Collider other) {
		if (other.tag == Tags.PLAYER) {
            // Do something.
            // Like give the player his booster upgrade.

            GameObject spawnedParticle = Instantiate<GameObject>(particleOnDeath);
            spawnedParticle.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}
