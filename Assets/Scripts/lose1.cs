using UnityEngine;
using System.Collections;


public class lose1 : MonoBehaviour {

	AudioSource audioSource;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		//体力０で消滅
		if(PlayerAp1.armorPoint <= 0){
			//爆発エフェクトを表示する
			Instantiate(explosion, transform.position, transform.rotation);
			audioSource.PlayOneShot(audioSource.clip);
			gameObject.SetActive (false);
		}
			
	}
}
