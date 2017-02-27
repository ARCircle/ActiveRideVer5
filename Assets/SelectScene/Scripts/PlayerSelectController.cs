using UnityEngine;
using System.Collections;

public class PlayerSelectController : MonoBehaviour {

		public int MinLane;
		public int MaxLane;

		static int targetLane = 2;
		public GameObject Selecter1;
		public GameObject Selecter2;
		public GameObject Selecter3;
		public GameObject States1;
		public GameObject States2;
		public GameObject States3;
		public GameObject GameStart;

		private AudioSource audioSource1;
		private AudioSource audioSource2;
		private AudioSource audioSource3;
		private AudioSource audioSource4;
		

		int flag = 0;

		float timer;


		void Start () {
			GameStart.SetActive (false);

			AudioSource[] audioSources = GetComponents<AudioSource>();
			audioSource1 = audioSources[0];
			audioSource2 = audioSources[1];
			audioSource3 = audioSources[2];
			audioSource4 = audioSources[3];
		}

		void Update () {

		if (Input.GetKeyDown ("a")) {
			audioSource1.PlayOneShot(audioSource1.clip);
			MoveToLeft ();
		}
		if (Input.GetKeyDown ("d")) {
			audioSource1.PlayOneShot(audioSource1.clip);
			MoveToRight ();
		}

		if(flag == 0 && Input.GetButtonDown ("U")) {
			audioSource4.PlayOneShot(audioSource4.clip);
			CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
				Application.LoadLevel ("SelectMenu");
			});
		}

			if (flag == 0 && Input.GetButtonDown ("Lock")) {
			audioSource2.PlayOneShot(audioSource2.clip);
			GameStart.SetActive (true);
			flag = 1;
		}

		if (flag == 1){

			//時間経過開始
			timer += Time.deltaTime;

			if(Input.GetButtonDown ("Jump")) {
				audioSource4.PlayOneShot(audioSource4.clip);
				GameStart.SetActive (false);
				flag = 0;
				timer = 0;
			}
		
			//時間一定経過で受付
			if(timer > 1){


				if(Input.GetButtonDown ("Lock")) {
					audioSource3.PlayOneShot(audioSource3.clip);
					timer = 0;
					Selectnumber ();
					CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
					Application.LoadLevel ("Main");
					});
				}

				if(Input.GetButtonDown ("Jump")) {
					audioSource4.PlayOneShot(audioSource4.clip);
					timer = 0;
					GameStart.SetActive (false);
					flag = 0;
				}


			}

		}



			if (targetLane == 2) {
				Selecter2.SetActive (true);
				Selecter1.SetActive (false);
				Selecter3.SetActive (false);
				States1.SetActive (false);
				States2.SetActive (true);
				States3.SetActive (false);
				Selectnumber ();
			}

			if (targetLane == 1) {
				Selecter3.SetActive (false);
				Selecter2.SetActive (false);
				Selecter1.SetActive (true);
				States1.SetActive (true);
				States2.SetActive (false);
				States3.SetActive (false);
				Selectnumber ();
			}

			if (targetLane == 3) {
				Selecter1.SetActive (false);
				Selecter2.SetActive (false);
				Selecter3.SetActive (true);
				States1.SetActive (false);
				States2.SetActive (false);
				States3.SetActive (true);
				Selectnumber ();
			}


			
		}

		public static int Selectnumber(){
			return targetLane;
		}



		public void MoveToLeft(){
		if (targetLane > MinLane)
				targetLane--;
		}

		public void MoveToRight(){
			if (targetLane < MaxLane)
				targetLane++;
		}

	}
