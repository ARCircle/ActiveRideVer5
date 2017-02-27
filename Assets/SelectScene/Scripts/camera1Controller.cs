using UnityEngine;
using System.Collections;

public class camera1Controller : MonoBehaviour {

	public GameObject camera1;
	public GameObject camera2;
	public GameObject camera3;
	public GameObject camera4;
	public GameObject camera5;
	public GameObject camera6;
	public GameObject camera7;
	public ParticleSystem smoke;

	int scene = 1;
	static int aniflag1 = 0;
	static int aniflag2 = 0;
	static int aniflag3 = 0;
	static int craflag = 0;
	int flag = 0;

	float time;

	void Start () {

		//smoke = GetComponent<ParticleSystem> ();
		camera1.SetActive (true);
		camera2.SetActive (false);
		camera3.SetActive (false);
		camera4.SetActive (false);
		camera5.SetActive (false);
		camera6.SetActive (false);
		camera7.SetActive (false);
		smoke.Stop ();
		time = 0.0f;
		flag1 ();
		flag2 ();
		flag3 ();
		flag4 ();
	
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;
	
		if(scene == 1 && time >= 10.0f){

			camera1.SetActive (false);
			camera2.SetActive (true);
			scene = 2;
			time = 0.0f;
		}

		if (scene == 2 && time >= 3.0f) {

				camera2.SetActive (false);
				camera3.SetActive (true);
				
				scene = 3;
				time = 0.0f;

		}

		if(scene == 3 && time >= 1.0f){

			camera3.SetActive(false);
			camera4.SetActive (true);
			scene = 4;
			time = 0.0f;
		}

		if(scene == 4 && time >= 1.0f){
			camera4.SetActive(false);
			camera5.SetActive (true);
			scene = 5;
			time = 0.0f;
		}

		if(scene == 5 && time >= 2.5f){

			camera5.SetActive(false);
			camera6.SetActive (true);
			scene = 6;
			aniflag1 = 1;
			flag1 ();
			smoke.Play ();
			time = 0.0f;
		}

		if(scene == 6 && time >= 8.0f){

			camera6.SetActive(false);
			camera7.SetActive (true);
			scene = 7;
			time = 0.0f;
		}

		if (scene == 7 && time >= 0.05f && flag == 0) {
			aniflag2 = 1;
			flag2 ();
			flag = 1;
			time = 0.0f;
		}

		if (scene == 7 && time >= 4.0f && flag == 1) {
			aniflag3 = 1;
			flag3 ();
			craflag = 1;
			flag4 ();
		}
			
	}

	public static int flag1(){
		return aniflag1;
	}

	public static int flag2(){
		return aniflag2;
	}

	public static int flag3(){
		return aniflag3;
	}

	public static int flag4(){
		return craflag;
	}
}
