using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stagekaeruyo : MonoBehaviour {

	int stagedayo;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage3;

	// Use this for initialization
	void Start () {
		stagedayo = StorySelectController.Selectnumber ();

		switch (stagedayo) {
		case 1:
			stage1.SetActive (true);
			stage2.SetActive (false);
			stage3.SetActive (false);
			break;
		case 2:
			stage1.SetActive (false);
			stage2.SetActive (true);
			stage3.SetActive (false);
			break;
		case 3:
			stage1.SetActive (false);
			stage2.SetActive (false);
			stage3.SetActive (true);
			break;

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
