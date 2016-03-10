using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class audio : MonoBehaviour {

	public AudioClip impact;
	AudioSource Audio;
	
	void Start() {
		Audio = GetComponent<AudioSource>();
	}
	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			Audio.PlayOneShot (impact, 1.0f);
		}
    }
	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			Audio.Stop ();
		}
	}
}
