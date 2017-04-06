using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot_U : MonoBehaviour {

	public Camera MainCamera1;

	public Image gaugeImage1;
	public Image gaugeImage2;

	public Text blinkTextOK1;
	public Text blinkTextOK2;

	public Color myWhite;
	public Color myRed;

	public GameObject shot1;
	public GameObject shot2;
	public GameObject muzzle;
	public GameObject muzzle1;
	public GameObject muzzleFlash1;
	public GameObject muzzleFlash2;


	float shotInterval1 = 0;
	float shotInterval2 = 0;
	public float shotIntervalMax1 = 2.0F;
	public float shotIntervalMax2 = 20.0F;

	public static int Shoot2OK;

	AudioSource audioSource;
	//AudioSource audioSource2;

	//GameObject CameraParent;
	//Quaternion defaultCameraRot;

	// Use this for initialization
	void Start () {

		audioSource = gameObject.GetComponent<AudioSource>();
		Shoot2OK = 1;

		//カメラ初期角度保存
		//CameraParent = Camera.main.transform.parent.gameObject;
		//defaultCameraRot = CameraParent.transform.localRotation;

	}

	// Update is called once per frame
	void Update () {

		//発射間隔を設定する
		shotInterval1 += Time.deltaTime;
		shotInterval2 += Time.deltaTime;
		//発射間隔をゲージに反映
		gaugeImage1.fillAmount = shotInterval1 / shotIntervalMax1;
		gaugeImage2.fillAmount = shotInterval2 / shotIntervalMax2;
		blinkTextOK1.color = new Color(1, 1, 1, 0);
		blinkTextOK2.color = new Color(1, 1, 1, 0);
		gaugeImage1.color =  myRed;
		gaugeImage2.color =  myRed;

		if(shotInterval1 >= shotIntervalMax1){

			blinkTextOK1.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
			gaugeImage1.color =   myWhite;

			//弾を発射する
			if( Input.GetButton("Fire1") ){
				shotInterval1 = 0;
				Instantiate(shot1, muzzle.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot1, muzzle1.transform.position, MainCamera1.transform.rotation);


				//Debug.Log ("撃ったよ");

				//マズルフラッシュを表示する
				Instantiate(muzzleFlash1, muzzle.transform.position, transform.rotation);
				Instantiate(muzzleFlash1, muzzle1.transform.position, transform.rotation);


				//SEを再生する
				//audioSource.Play();
				//音を重ねて再生する
				audioSource.PlayOneShot(audioSource.clip);
			}
		}

		if (shotInterval2 >= shotIntervalMax2) {

			Shoot2OK = 2;

			blinkTextOK2.color = new Color (1, 1, 1, Mathf.PingPong (Time.time, 1));
			gaugeImage2.color = myWhite;

			//弾を発射する
			if (Input.GetButton("ShootMode1") && Input.GetButton("Fire1")) {

				//CameraParent.transform.localRotation = Quaternion.Slerp (CameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

				shotInterval2 = 0;
				Instantiate (shot2, muzzle.transform.position, MainCamera1.transform.rotation);
				Instantiate (shot2, muzzle1.transform.position, MainCamera1.transform.rotation);

				//Debug.Log ("撃ったよ");

				//マズルフラッシュを表示する
				Instantiate (muzzleFlash2, muzzle.transform.position, transform.rotation);
				Instantiate (muzzleFlash2, muzzle1.transform.position, transform.rotation);

				//SEを再生する
				//audioSource.Play();
				//音を重ねて再生する
				audioSource.PlayOneShot (audioSource.clip);
			}

		} else {
			Shoot2OK = 1;
		}


	}
}
