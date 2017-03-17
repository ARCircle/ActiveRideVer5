using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelect : MonoBehaviour {

	int flag = 0;
	int music;
	int deff;

	public GameObject Audio1;
	public GameObject Audio2;
	public GameObject Audio3;

	void Start () {

		music = StageSelectController.Musicnumber ();
		deff = music;

	}
	
	// Update is called once per frame
	void Update () {

		music = StageSelectController.Musicnumber ();

		if (flag == 0) {

			switch (music) {

			case 1:
				Audio1.SetActive (true);
				Audio2.SetActive (false);
				Audio3.SetActive (false);
				flag = 1;
				break;
			case 2:
				Audio2.SetActive (true);
				Audio1.SetActive (false);
				Audio3.SetActive (false);
				flag = 1;
				break;
			case 3:
				Audio3.SetActive (true);
				Audio2.SetActive (false);
				Audio1.SetActive (false);
				flag = 1;
				break;
			}
		}

		if (flag == 1) {

			if (deff != music) {
				flag = 0;
			}

			deff = music;
		}
	}
}
