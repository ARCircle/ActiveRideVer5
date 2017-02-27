using UnityEngine;
using System.Collections;

public class SelectController : MonoBehaviour {

	public GameObject Banshi; 
	public GameObject Unicorn; 
	public GameObject Fenecs; 

	int selectnumber = PlayerSelectController.Selectnumber ();

	void Awake () {

		if (selectnumber == 2) {
			Banshi.SetActive (false);
			Unicorn.SetActive (true);
			Fenecs.SetActive (false);
		}

		if (selectnumber == 1) {
			Banshi.SetActive (true);
			Unicorn.SetActive (false);
			Fenecs.SetActive (false);
		}

		if (selectnumber == 3) {
			Banshi.SetActive (false);
			Unicorn.SetActive (false);
			Fenecs.SetActive (true);
		}



	}
}
