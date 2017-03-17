using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StorySelectController : MonoBehaviour {

	public int MinLane;
	public int MaxLane;
	public int Minmusic;
	public int Maxmusic;

	static int targetLane = 1;
	public GameObject Selecter1;
	public GameObject Selecter2;
	public GameObject Selecter3;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage3;
	public GameObject GameStart;

	private AudioSource audioSource1;
	private AudioSource audioSource2;
	private AudioSource audioSource3;
	private AudioSource audioSource4;

	int flag = 0;
	int accept = 0;

	float time;
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

		if (accept == 0) {

			if (Input.GetAxis ("Horizontal") < 0.0f) {
				//timer = Time.deltaTime;
				audioSource1.PlayOneShot (audioSource1.clip);
				MoveToLeft ();
				Debug.Log ("1left");
				Debug.Log (targetLane);
				accept = 1;
				time = 0;
			}

			if (Input.GetAxis ("Horizontal") > 0.0f) {
				audioSource1.PlayOneShot (audioSource1.clip);
				MoveToRight ();
				Debug.Log ("1right");
				Debug.Log (targetLane);
				accept = 1;
				time = 0;
			}
				

		}

		if (accept == 1) {
			time += Time.deltaTime;

			if (time >= 0.5f) {
				accept = 0;
			}
		}

		if(flag == 0 && Input.GetButtonDown ("Cancel")) {
			audioSource4.PlayOneShot(audioSource4.clip);

			CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
				SceneManager.LoadScene ("SelectMenu");
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
			if(timer > 1.0f){


				if(Input.GetButtonDown ("Lock")) {
					audioSource3.PlayOneShot(audioSource3.clip);
					timer = 0;
					Selectnumber ();

					switch (targetLane) {

					case 1:
						CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
							SceneManager.LoadScene ("2PlayerModeStage1");
						});
						break;
					case 2:
						CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
							SceneManager.LoadScene ("2PlayerModeStage2");
						});
						break;
					case 3:
						CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
							SceneManager.LoadScene ("2PlayerModeStage3");
						});
						break;

					}
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
			stage2.SetActive (true);
			stage1.SetActive (false);
			stage3.SetActive (false);
			Selectnumber ();
		}

		if (targetLane == 1) {
			Selecter3.SetActive (false);
			Selecter2.SetActive (false);
			Selecter1.SetActive (true);
			stage2.SetActive (false);
			stage1.SetActive (true);
			stage3.SetActive (false);
			Selectnumber ();
		}

		if (targetLane == 3) {
			Selecter1.SetActive (false);
			Selecter2.SetActive (false);
			Selecter3.SetActive (true);
			stage2.SetActive (false);
			stage1.SetActive (false);
			stage3.SetActive (true);
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
