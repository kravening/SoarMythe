﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AudioSourceController : MonoBehaviour
{
	[SerializeField]
	private List<AudioClip> audioList = new List<AudioClip>();
	[SerializeField]
    private float audioVolume, minPitch, maxPitch;
	[SerializeField]
	private bool autoStopSound, randomisePitch;


	private AudioSource audioSource;
	private int lastIndexPlayed;
	private int audioReRolls = 3;

	public bool isPlaying = false;
	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = audioVolume;
		if (audioList.Count != 0)
		{
			lastIndexPlayed = Random.Range(0, audioList.Count);
		}
		else
		{
			print("there is nothing in the audioList you dingus");
		}
	}

    /// <summary>
    /// The most intensive, uses a for loop to find the right sound.
    /// By index is the preferred way.
    /// </summary>
    /// <param name="name">Name of the song.</param>
    public void ChangeAudioSourceByName(string name) {
        for (int i = 0; i < audioList.Count; i++) {
            AudioClip clip = audioList[i];

            if (clip.name == name) {
                PlayAudioClip(i);
                return;
            }
        }

        print(name + " is not a valid sound you dingus"); // Gotta follow the dingus road.
    }

	public void ChangeAudioSourceByIndex(int index)
	{
		//isPlaying = true;
		if (index <= audioList.Count && audioList.Count != 0)
		{
			
			PlayAudioClip(index);
		}
		else
		{
			print("index given is too large, or there might be nothing in the audioList you dingus");
		}
	}

	public void ChangeAudioSourceRandom()
	{
		int ReRolls = audioReRolls;
		int randomIndex = Random.Range(0, audioList.Count);
		if (audioList.Count != 0)
		{
			while(randomIndex == lastIndexPlayed && ReRolls <= 0)
			{//check if last sound played is the same
				randomIndex = Random.Range(0, audioList.Count); 		 //reroll index
				ReRolls--; 												//for the small chance it rerolls the same audio file forever
			}
			lastIndexPlayed = randomIndex;                           // sets new lastPlayedIndex
			PlayAudioClip(randomIndex);
		}
		else
		{
			print("there is nothing in the audioList you dingus");
		}

	}

	public void StopAudio(){
		audioSource.Stop();
		//isPlaying = false;
	}

	private void PlayAudioClip(int indexGiven){
		if (autoStopSound)
		{
			StopAudio();	//stop sound here
		}
		//isPlaying = true;
		audioSource.clip = audioList[indexGiven];				 //change sound here
		audioSource.Play();                                     //play sound here
		RandomisePitch();
	}

	private void RandomisePitch()
	{
		if (randomisePitch)
		{
			audioSource.pitch = Random.Range(minPitch, maxPitch);
		}
	}
}
