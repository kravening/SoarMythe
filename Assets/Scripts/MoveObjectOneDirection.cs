using UnityEngine;
using System.Collections;

public class MoveObjectOneDirection : MonoBehaviour {

    [SerializeField]float speed;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += transform.forward * speed * Time.deltaTime; // or transform.position, both would work
    }
}
