using UnityEngine;
using System.Collections;


public class Player2Controller : MonoBehaviour {


	public GameObject Banshi; 
	public GameObject Unicorn; 
	public GameObject Fenecs; 

	int selectnumber2 = DoublePlayerSelectController.Selectnumber2 ();

	void Update () {

		if (selectnumber2 == 2) {
			Banshi.SetActive (false);
			Unicorn.SetActive (true);
			Fenecs.SetActive (false);
		}

		if (selectnumber2 == 1) {
			Banshi.SetActive (true);
			Unicorn.SetActive (false);
			Fenecs.SetActive (false);
		}

		if (selectnumber2 == 3) {
			Banshi.SetActive (false);
			Unicorn.SetActive (false);
			Fenecs.SetActive (true);
		}

	}
}
