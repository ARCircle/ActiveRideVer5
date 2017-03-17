using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMselectController : MonoBehaviour {

	int music;
	int flag = 0;

	private AudioSource audioSource1;
	private AudioSource audioSource2;
	private AudioSource audioSource3;



	void Start () {

		music = StageSelectController.Musicnumber ();

		AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource1 = audioSources[0];
		audioSource2 = audioSources[1];
		audioSource3 = audioSources[2];

		switch (music) {

		case 1: 
			audioSource1.PlayOneShot (audioSource1.clip);
			flag = 1;
			break;
		case 2:
			audioSource2.PlayOneShot (audioSource2.clip);
			flag = 1;
			break;
		case 3:
			audioSource3.PlayOneShot (audioSource3.clip);
			flag = 1;
			break;
		}

	}
	

	void Update () {

	}
}
