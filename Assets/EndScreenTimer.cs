using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndScreenTimer : MonoBehaviour {
	private float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey || timer >= 15f) {
			SceneManager.LoadScene ("MainMenu");
		}else{
			timer += Time.deltaTime;
		}
	}
}
