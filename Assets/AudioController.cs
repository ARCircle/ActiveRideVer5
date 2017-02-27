using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {


	private AudioSource audioSource1;
	private AudioSource audioSource2;
	private AudioSource audioSource3;
	private AudioSource audioSource4;
	private AudioSource audioSource5;
	private AudioSource audioSource6;
	private AudioSource audioSource7;

	float time = 0.0f;
	int flag = 0;

	// Use this for initialization
	void Start () {
	
		AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource1 = audioSources[0];		//キュピーン
		audioSource2 = audioSources[1];		//ガシャン１
		audioSource3 = audioSources[2];		//出発
		audioSource4 = audioSources[3];		//蒸気
		audioSource5 = audioSources[4];		//ドゥン
		audioSource6 = audioSources[5];		//ガシャン２
		audioSource7 = audioSources[6];		//チャージ

	}
	
	// Update is called once per frame
	void Update () {
	
		time += Time.deltaTime;

		if (time >= 11.0f && flag == 0) {
			audioSource1.PlayOneShot (audioSource1.clip);
			flag = 1;
		}


		//3シーン連続切り替え
		if (time >= 13.0f && flag == 1) {
			audioSource6.PlayOneShot (audioSource6.clip);
			flag = 2;
		}

		/*if (time >= 14.0f && flag == 2) {
			audioSource6.PlayOneShot (audioSource6.clip);
			flag = 3;
		}

		if (time >= 15.0f && flag == 3) {
			audioSource6.PlayOneShot (audioSource6.clip);
			flag = 4;
		}*/
		//3シーン連ぞっく切り替え終わり


		if(time >= 18.0f && flag == 4){
			audioSource2.PlayOneShot (audioSource2.clip);	//煙前ガシャン
			flag = 5;
		}

		if (time >= 20.0f && flag == 5) {
			audioSource4.PlayOneShot (audioSource4.clip);	//煙
			flag = 6;
		}

		if (time >= 13.0f && flag == 6) {
			audioSource6.PlayOneShot (audioSource6.clip);	//チャージ前ガシャン
			flag = 7;
		}

		if (time >= 27.0f && flag == 7) {
			audioSource7.PlayOneShot (audioSource7.clip);	//チャージ
			flag = 8;
		}

		if (time >= 31.0f && flag == 8) {
			audioSource3.PlayOneShot (audioSource3.clip);	//出発
			flag = 9;
		}
}
}