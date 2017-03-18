using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {


	public ParticleSystem smoke;
	public ParticleSystem smoke2;
	public ParticleSystem light;

	float time;
	int parflag = 0;
	static int claflag = 0;

	void Start () {

		smoke.Stop ();
		smoke2.Stop ();
		light.Stop ();
		flag();
	}
	

	void Update () {

		time += Time.deltaTime;

		if(time >= 10.0 && parflag == 0){
			light.Play();
			parflag = 1;
		}

		if(time >= 21.5 && parflag == 1){
			smoke.Play();
			parflag = 2;
		}

		if (time >= 27.5f) {
			claflag = 1;
			flag ();
		}

		if (time >= 32.5) {
			smoke2.Play ();
		}
	}


	public static int flag(){
		return claflag;
	}
}
