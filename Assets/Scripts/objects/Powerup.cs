using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    GameObject particleOnDeath;

    void OnTriggerEnter(Collider other) {
		if (other.tag == Tags.PLAYER) {
            // Do something.
            // Like give the player his booster upgrade.

            GameObject spawnedParticle = Instantiate<GameObject>(particleOnDeath);
            spawnedParticle.transform.position = transform.position;

			PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement> ();
			playerMovement.PowerUp ();

            Destroy(gameObject);
        }
    }
}
