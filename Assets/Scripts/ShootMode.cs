using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootMode : MonoBehaviour {

	//public int FieldOfView = 60;
	//public Camera MainCamera;

	public Image right;
	public Image left;
	public Image LockOnCursor;
	public Image Lock_Shoot;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("ShootMode1")) {
			Camera.main.fieldOfView = 40;
			right.enabled = true;
			left.enabled = true;
			LockOnCursor.enabled = false;
			Lock_Shoot.enabled = true;

			//Mathf.Clamp (Camera.main.fieldOfView += 2, 40, 60);
			//Camera.main.fieldOfView += 2; 
		} else {
			Camera.main.fieldOfView = 60;
			right.enabled = false;
			left.enabled = false;
			LockOnCursor.enabled = true;
			Lock_Shoot.enabled = false;
			//Mathf.Clamp (Camera.main.fieldOfView -= 2, 40, 60);
		}
	}
}
