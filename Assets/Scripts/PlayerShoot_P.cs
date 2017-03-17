using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot_P : MonoBehaviour {

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
	public GameObject muzzle2;
	public GameObject muzzle3;
	public GameObject muzzle4;
	public GameObject muzzleFlash1;
	public GameObject muzzleFlash2;


	float shotInterval1 = 0;
	float shotInterval2 = 0;
	public float shotIntervalMax1 = 2.0F;
	public float shotIntervalMax2 = 20.0F;

	AudioSource audioSource;
	//AudioSource audioSource2;

	// Use this for initialization
	void Start () {

		audioSource = gameObject.GetComponent<AudioSource>();
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
				Instantiate(shot1, muzzle2.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot1, muzzle3.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot1, muzzle4.transform.position, MainCamera1.transform.rotation);

				//Debug.Log ("撃ったよ");

				//マズルフラッシュを表示する
				Instantiate(muzzleFlash1, muzzle.transform.position, transform.rotation);
				Instantiate(muzzleFlash1, muzzle1.transform.position, transform.rotation);
				Instantiate(muzzleFlash1, muzzle2.transform.position, transform.rotation);
				Instantiate(muzzleFlash1, muzzle3.transform.position, transform.rotation);
				Instantiate(muzzleFlash1, muzzle4.transform.position, transform.rotation);

				//SEを再生する
				//audioSource.Play();
				//音を重ねて再生する
				audioSource.PlayOneShot(audioSource.clip);
			}
		}

		if(shotInterval2 >= shotIntervalMax2){

			blinkTextOK2.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
			gaugeImage2.color =   myWhite;

			//弾を発射する
			if(Input.GetKey (KeyCode.B)){
				shotInterval2 = 0;
				Instantiate(shot2, muzzle.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot2, muzzle1.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot2, muzzle2.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot2, muzzle3.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot2, muzzle4.transform.position, MainCamera1.transform.rotation);

				//Debug.Log ("撃ったよ");

				//マズルフラッシュを表示する
				Instantiate(muzzleFlash2, muzzle.transform.position, transform.rotation);
				Instantiate(muzzleFlash2, muzzle1.transform.position, transform.rotation);
				Instantiate(muzzleFlash2, muzzle2.transform.position, transform.rotation);
				Instantiate(muzzleFlash2, muzzle3.transform.position, transform.rotation);
				Instantiate(muzzleFlash2, muzzle4.transform.position, transform.rotation);

				//SEを再生する
				//audioSource.Play();
				//音を重ねて再生する
				audioSource.PlayOneShot(audioSource.clip);
			}
		}


	}
}
