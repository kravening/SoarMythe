using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {
    [SerializeField, Tooltip("Do I win? If false I trigger a loss.")]
    bool winTrigger = true;

	private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            if (winTrigger) {
				Debug.Log ("wauw");
				SceneManager.LoadScene ("EndScene");
            } else {
                GameController.LoseGame(gameObject.name);
            }
        }
    }
}
