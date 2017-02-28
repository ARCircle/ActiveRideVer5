using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TitleScene : MonoBehaviour {

	public Text blinkText;

	public GameObject ModalWindow;
	private bool ModalDiagloFrag = true;

	float timer;

	GameObject MainSoundObject;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {

		MainSoundObject = GameObject.Find ("MainSoundObject");

		//ModalWindow.SetActive(false);
		audioSource = GetComponent<AudioSource>();
		timer = 0;
	}

	// Update is called once per frame
	void Update () {

		MainSoundObject.SetActive (true);

		timer += Time.deltaTime;

		//一定秒たったら操作を受け付ける
		if (timer > 6) {

			//ボタンを押したら遷移
			if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.DownArrow)) {
        
				audioSource.PlayOneShot(audioSource.clip);
				//Application.LoadLevel("SelectMenu");
				timer = 0;
				CameraFade.StartAlphaFade (Color.black, false, 0.3f, 0.3f, () => {
					Application.LoadLevel ("SelectMenu");
				});
			}

		}


		//ボタンを押させるためのメッセージを点滅させる
		blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
	}
}
