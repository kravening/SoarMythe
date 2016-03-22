using UnityEngine;
using System.Collections;

public class sunPull : MonoBehaviour {
    [SerializeField][Tooltip("How fast do I pull ToPull to ToPullTo?")]
    float speed = 0.1f;
    
    [SerializeField][Tooltip("What will I pull towards ToPullTo?")]
    Transform toPull;

    [SerializeField][Tooltip("What will I pull ToPull towards?")]
    Transform toPullTo;

    public bool Pull = false;

    bool firstRun = true;

    Rigidbody rb;

    void Start() {
        rb = toPull.gameObject.GetComponent<Rigidbody>();
    }

	void Update () {
        if (Pull) {
            toPull.position = Vector3.Lerp(toPull.position, toPullTo.position, Time.deltaTime * speed);
            if (firstRun) {
                firstRun = false;
                rb.useGravity = false;
                rb.velocity = new Vector3(0, 0, 0);

                toPull.gameObject.GetComponent<PlayerMovement>().IsAlive = false;
            }
        } else {
            if (!firstRun) {
                firstRun = true;

                rb.useGravity = true;

                toPull.gameObject.GetComponent<PlayerMovement>().IsAlive = true;
            }
        }
	}
}
