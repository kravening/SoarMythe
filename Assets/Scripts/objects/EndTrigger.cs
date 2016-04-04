using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {
    [SerializeField, Tooltip("Do I win? If false I trigger a loss.")]
    bool winTrigger = true;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            if (winTrigger) {
                GameController.WinGame();
            } else {
                GameController.LoseGame();
            }
        }
    }
}
