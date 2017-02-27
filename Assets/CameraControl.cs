using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		//カメラを回転させる
		Camera.main.transform.Rotate(-Input.GetAxis("Vertical2"), 0, 0);
		Camera.main.transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

	}
}
