using UnityEngine;
using System.Collections;

public class LightShake : MonoBehaviour {
	[SerializeField]Transform pos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = pos.position;
		newPos.x += Random.Range(-.01f,.01f);
		newPos.y += Random.Range(-.01f,.01f);
		newPos.z += Random.Range(-.01f,.01f) + .1f;
		transform.position = newPos;
	}
}
