using UnityEngine;
using System.Collections;

public class PlayerShoot_U2 : MonoBehaviour {

	public GameObject shot;
	public GameObject muzzle1;
	public GameObject muzzle2;

	public GameObject muzzleFlash;
	public Camera MainCamera2;
	
	float shotInterval = 0;
	float shotIntervalMax = 1.5F;
	
	AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
	
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//発射間隔を設定する
		shotInterval += Time.deltaTime;
	
		if(shotInterval > shotIntervalMax){
			
			//弾を発射する
			if( Input.GetButton("Fire2") ){
				
				Instantiate(shot, muzzle1.transform.position, MainCamera2.transform.rotation);
				Instantiate(shot, muzzle2.transform.position, MainCamera2.transform.rotation);

				shotInterval = 0;
				
				//マズルフラッシュを表示する
				Instantiate(muzzleFlash, muzzle1.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle2.transform.position, transform.rotation);

				
				//SEを再生する
				//audioSource.Play();
				
				//音を重ねて再生する
				audioSource.PlayOneShot(audioSource.clip);
			}
			
		}
	}
}
