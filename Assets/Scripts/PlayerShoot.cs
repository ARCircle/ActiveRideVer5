﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

	public Camera MainCamera1;

	public Image gaugeImage;

	public GameObject shot;
	public GameObject muzzle;
	public GameObject muzzle1;
	public GameObject muzzle2;
	public GameObject muzzle3;
	public GameObject muzzle4;
	public GameObject muzzleFlash;


	float shotInterval = 0;
	public float shotIntervalMax = 5.0F;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		
		//発射間隔を設定する
		shotInterval += Time.deltaTime;
		//Debug.Log (shotInterval);
		if(shotInterval >= shotIntervalMax){

			//弾を発射する
			if( Input.GetButton("Fire1") ){
				shotInterval = 0;
				Instantiate(shot, muzzle.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot, muzzle1.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot, muzzle2.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot, muzzle3.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot, muzzle4.transform.position, MainCamera1.transform.rotation);

				//Debug.Log ("撃ったよ");

				//マズルフラッシュを表示する
				Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle1.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle2.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle3.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle4.transform.position, transform.rotation);

				//SEを再生する
				//audioSource.Play();

				//音を重ねて再生する
				audioSource.PlayOneShot(audioSource.clip);


				gaugeImage.fillAmount = shotInterval / shotIntervalMax;
			}

		}
	}
}
