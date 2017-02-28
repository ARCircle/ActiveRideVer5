using UnityEngine;
using System.Collections;

public class DoublePlayerSelectController : MonoBehaviour {

	public int MinLane;
	public int MaxLane;

	static int onetargetLane = 1;
	public GameObject Selecter11;
	public GameObject Selecter12;
	public GameObject Selecter13;
	public GameObject States11;
	public GameObject States12;
	public GameObject States13;
	static int twotargetLane = 3;
	public GameObject Selecter21;
	public GameObject Selecter22;
	public GameObject Selecter23;
	public GameObject States21;
	public GameObject States22;
	public GameObject States23;
	public GameObject GameStart1;
	public GameObject GameStart2;
	public GameObject Ready;

	int flag1 = 0;
	int flag2 = 0;

	float x;
	float y;

	float timer;

	private AudioSource audioSource1;
	private AudioSource audioSource2;
	private AudioSource audioSource3;
	private AudioSource audioSource4;

	void Start () {
		GameStart1.SetActive (false);
		GameStart2.SetActive (false);

		AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource1 = audioSources[0];
		audioSource2 = audioSources[1];
		audioSource3 = audioSources[2];
		audioSource4 = audioSources[3];

	}

	void Update () {

		if (Input.GetAxis ("Horizontal")<0.0f) {
			audioSource1.PlayOneShot (audioSource1.clip);
			MoveToLeft1 ();
		}

		if (Input.GetAxis ("Horizontal")>0.0f) {
			audioSource1.PlayOneShot (audioSource1.clip);
			MoveToRight1 ();
		}

		if (Input.GetAxis ("Horizontal2")<0.0f) {
			audioSource1.PlayOneShot (audioSource1.clip);
			MoveToLeft2 ();
		}

		if (Input.GetAxis ("Horizontal2")>0.0f) {
			audioSource1.PlayOneShot (audioSource1.clip);
			MoveToRight2 ();
		}


		if(flag1 == 0 && flag2 ==0  && Input.GetButtonDown ("Cancel")) {
				audioSource4.PlayOneShot(audioSource4.clip);
				CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
					Application.LoadLevel ("SelectMenu");
				});
		}

		if (Input.GetButtonDown ("Lock2")) {
			audioSource2.PlayOneShot (audioSource2.clip);
			GameStart2.SetActive (true);
			flag2 = 1;
		}

		if (Input.GetButtonDown ("Lock")) {
			audioSource2.PlayOneShot (audioSource2.clip);
			GameStart1.SetActive (true);
			flag1 = 1;
		}


		if (flag1 == 1 && Input.GetButtonDown ("Jump")) {
			audioSource4.PlayOneShot (audioSource4.clip);
			GameStart1.SetActive (false);
			flag1 = 0;
		}

		if (flag2 == 1 && Input.GetButtonDown ("Jump2")) {
			audioSource4.PlayOneShot (audioSource4.clip);
			GameStart2.SetActive (false);
			flag2 = 0;
		}

		if (flag1 == 1 && flag2 == 1) {

			//時間経過開始
			timer += Time.deltaTime;

			Ready.SetActive (true);


			if(timer > 1){
				
				if (Input.GetButtonDown ("Lock")) {
					audioSource3.PlayOneShot (audioSource3.clip);
					timer = 0;
					Selectnumber1 ();
					Selectnumber2 ();
					CameraFade.StartAlphaFade (Color.black, false, 0.6f, 0.6f, () => {
						Application.LoadLevel ("2PlayerMode");
					});

				}
			}
		} else {
			Ready.SetActive (false);
				timer = 0;
		}



		if (onetargetLane == 2) {
			Selecter12.SetActive (true);
			Selecter11.SetActive (false);
			Selecter13.SetActive (false);
			States11.SetActive (false);
			States12.SetActive (true);
			States13.SetActive (false);
			Selectnumber1 ();
		}

		if (onetargetLane == 1) {
			Selecter13.SetActive (false);
			Selecter12.SetActive (false);
			Selecter11.SetActive (true);
			States11.SetActive (true);
			States12.SetActive (false);
			States13.SetActive (false);
			Selectnumber1 ();
		}

		if (onetargetLane == 3) {
			Selecter11.SetActive (false);
			Selecter12.SetActive (false);
			Selecter13.SetActive (true);
			States11.SetActive (false);
			States12.SetActive (false);
			States13.SetActive (true);
			Selectnumber1 ();
		}


		if (twotargetLane == 2) {
			Selecter22.SetActive (true);
			Selecter21.SetActive (false);
			Selecter23.SetActive (false);
			States21.SetActive (false);
			States22.SetActive (true);
			States23.SetActive (false);
			Selectnumber2 ();
		}

		if (twotargetLane == 1) {
			Selecter23.SetActive (false);
			Selecter22.SetActive (false);
			Selecter21.SetActive (true);
			States21.SetActive (true);
			States22.SetActive (false);
			States23.SetActive (false);
			Selectnumber2 ();
		}

		if (twotargetLane == 3) {
			Selecter21.SetActive (false);
			Selecter22.SetActive (false);
			Selecter23.SetActive (true);
			States21.SetActive (false);
			States22.SetActive (false);
			States23.SetActive (true);
			Selectnumber2 ();
		}
	}

	public static int Selectnumber1(){
		return onetargetLane;
	}

	public static int Selectnumber2(){
		return twotargetLane;
	}

	public void MoveToLeft1(){
		if (onetargetLane - 1 == twotargetLane) {
			if (onetargetLane - 2 >= MinLane) {
				onetargetLane = onetargetLane - 2;
			}
		}else if (onetargetLane > MinLane)
			onetargetLane--;
	}

	public void MoveToRight1(){
		if (onetargetLane + 1 == twotargetLane) {
			if (onetargetLane + 2 <= MaxLane) {
				onetargetLane = onetargetLane + 2;
			}
		}else if (onetargetLane < MaxLane)
			onetargetLane++;
	}

	public void MoveToLeft2(){
		if (twotargetLane - 1 == onetargetLane) {
			if (twotargetLane - 2 >= MinLane) {
				twotargetLane = twotargetLane - 2;
			}
		}else if (twotargetLane > MinLane)
			twotargetLane--;
	}

	public void MoveToRight2(){
		if (twotargetLane + 1 == onetargetLane) {
			if (twotargetLane + 2 <= MaxLane) {
				twotargetLane = twotargetLane + 2;
			}
		}else if (twotargetLane < MaxLane)
			twotargetLane++;
	}
}