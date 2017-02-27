using UnityEngine;
using System.Collections;

public class PlayerShoot_U1 : MonoBehaviour {

	public Camera MainCamera1;

	public GameObject shot;
	public GameObject muzzle1;
	public GameObject muzzle2;
	public GameObject muzzleFlash;

	
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
			if( Input.GetButton("Fire1") ){
				Instantiate(shot, muzzle1.transform.position, MainCamera1.transform.rotation);
				Instantiate(shot, muzzle2.transform.position, MainCamera1.transform.rotation);
				shotInterval = 0;
				audioSource.PlayOneShot(audioSource.clip);
				//マズルフラッシュを表示する
				Instantiate(muzzleFlash, muzzle1.transform.position, transform.rotation);
				Instantiate(muzzleFlash, muzzle2.transform.position, transform.rotation);
				
				//SEを再生する
				//audioSource.Play();
				
				//音を重ねて再生する

			}
			
		}
	}
}
