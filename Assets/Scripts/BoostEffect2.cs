using UnityEngine;
using System.Collections;

public class BoostEffect2 : MonoBehaviour {

	public GameObject boostLight;
	
	// Use this for initialization
	void Start () {
	
		boostLight.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		bool flgBoost = false;
		
		//ブーストorジャンプ
		if (Input.GetButton ("Boost2") || Input.GetButton ("Jump2"))
			flgBoost = true;
			
		boostLight.SetActive (flgBoost);
	}
}
