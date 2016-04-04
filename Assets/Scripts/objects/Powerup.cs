using UnityEngine;

public class Powerup : MonoBehaviour {
    // Give the player some more feedback other than the power raise.
    [SerializeField, Tooltip("I will drop this particle upon my death.")]
    GameObject particleOnDeath;

    // If this is left empty it will do the default raise.
    [SerializeField, Tooltip("How much of a power raise should I give the player?")]
    int powerUp = 0;

    [SerializeField, Tooltip("Also add power so the player has more jump power?")]
    bool addPower = false;

    void OnTriggerEnter(Collider other) {
        // Wouldn't wanna try and give a random object a powerup.
		if (other.tag == Tags.PLAYER) {
            GameObject spawnedParticle = Instantiate<GameObject>(particleOnDeath);
            spawnedParticle.transform.position = transform.position;

			PowerContainer powerContainer = other.gameObject.GetComponent<PowerContainer> ();
            if (powerUp != 0)
                powerContainer.PowerUp(powerUp, addPower);
            else
                powerContainer.PowerUp();

            // My job is done, I've thrown a particle effect and given the player his boost.
            Destroy(gameObject);
        }
    }
}
