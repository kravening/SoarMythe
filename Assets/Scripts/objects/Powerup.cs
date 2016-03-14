using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    GameObject particleOnDeath;

    void OnTriggerEnter(Collider other) {
		if (other.tag == Tags.PLAYER) {
            GameObject spawnedParticle = Instantiate<GameObject>(particleOnDeath);
            spawnedParticle.transform.position = transform.position;

			PowerContainer powerContainer = other.gameObject.GetComponent<PowerContainer> ();
            powerContainer.PowerUp();

            Destroy(gameObject);
        }
    }
}
