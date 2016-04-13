using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {
	[SerializeField]private MovieTexture movieTexture;
	private AudioSource audioSource;
	private AudioClip movieAudio;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		movieAudio = movieTexture.audioClip;
		audioSource.clip = movieAudio;
		audioSource.volume = .98f;
		audioSource.Play ();
		movieTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey || !audioSource.isPlaying) {
			SceneManager.LoadScene ("Prototype_V4");
		}
	}

	void OnGUI(){
		if (movieTexture.isPlaying) {
			if (movieTexture != null && movieTexture.isPlaying) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), movieTexture, ScaleMode.StretchToFill);
			}
		}
	}
}
