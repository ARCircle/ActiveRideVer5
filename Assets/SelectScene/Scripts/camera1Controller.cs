using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class camera1Controller : MonoBehaviour {

	public GameObject camera1;
	public GameObject camera2;
	public GameObject camera3;
	public GameObject camera4;
	public GameObject camera5;
	public GameObject camera6;
	public GameObject camera7;
	public GameObject camera8;
	public GameObject camera9;
	public GameObject clashcamera1;
	public GameObject clashcamera2;
	public GameObject clashcamera3;

	int scene = 1;
	float time;
	int stage;

	void Start () {

		camera1.SetActive (true);
		camera2.SetActive (false);
		camera3.SetActive (false);
		camera4.SetActive (false);
		camera5.SetActive (false);
		camera6.SetActive (false);
		camera7.SetActive (false);
		camera8.SetActive (false);
		camera9.SetActive (false);
		clashcamera1.SetActive (false);
		clashcamera2.SetActive (false);
		clashcamera3.SetActive (false);
		time = 0.0f;

		stage = StorySelectController.Selectnumber ();
	}
	

	void Update () {

		time += Time.deltaTime;
	
		if(scene == 1 && time >= 10.0f){

			CameraFade.StartAlphaFade(Color.black, true, 5.0f, 0.5f, () => { camera1.SetActive (false); });
			camera2.SetActive (true);
			scene = 2;
			time = 0.0f;
		}

		if (scene == 2 && time >= 4.0f) {

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
			time = 0.0f;
		}

		if(scene == 6 && time >= 8.0f){

			camera6.SetActive(false);
			camera7.SetActive (true);
			scene = 7;
			time = 0.0f;
		}

		if(scene == 7 && time >= 2.0f){

			camera7.SetActive(false);
			clashcamera1.SetActive (true);
			scene = 8;
			time = 0.0f;
		}


		if(scene == 8 && time >= 3.8f){

			clashcamera1.SetActive(false);
			clashcamera2.SetActive (true);
			scene = 9;
			time = 0.0f;
		}
	
		if(scene == 9 && time >= 0.5f){

			//CameraFade.StartAlphaFade (Color.black, false, 5.0f, 1.5f, () => {
				clashcamera2.SetActive(false);
			//});
			clashcamera3.SetActive (true);
			scene = 10;
			time = 0.0f;
		}

		if (scene == 10 && time >= 3.0f) {

			clashcamera3.SetActive (false);
			camera8.SetActive (true);
			scene = 11;
			time = 0.0f;
		}

		if (scene == 11 && time >= 2.3f) {

			camera8.SetActive (false);
			camera9.SetActive (true);
			scene = 12;
			time = 0.0f;
		}

		if (scene == 12 && time >= 5.0f) {
			
			//SceneManager.LoadScene ("cockpit");

			switch (stage) {

			case 1:
					SceneManager.LoadScene ("cockpit_s");
				break;
			case 2:
					SceneManager.LoadScene ("cockpit_m");
				break;
			case 3:
					SceneManager.LoadScene ("cockpit_c");
				break;

			}
		
		}
	}


}
