using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {
    ParticleSystem particleSys;

    void Start() {
        particleSys = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (!particleSys.isPlaying) {
            Destroy(gameObject);
        }
    }
}
