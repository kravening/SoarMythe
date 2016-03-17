using UnityEngine;

public class BillboardObject : MonoBehaviour {
    private Transform myTransform;

    void Awake() {
        myTransform = gameObject.transform;
    }

    void Update() {
        myTransform.LookAt(Camera.main.transform.position);
    }

}