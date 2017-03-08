using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	GameObject CameraParent;
	Quaternion defaultCameraRot;
	//float timer = 0;

	// Use this for initialization
	void Start () {

		//カメラ初期方向記憶
		CameraParent = Camera.main.transform.gameObject;
		defaultCameraRot = CameraParent.transform.localRotation;

	}

	// Update is called once per frame
	void Update () {


		if (Input.anyKey) {
			//カメラを回転させる
			Camera.main.transform.Rotate (-Input.GetAxis ("Vertical3"), 0, 0);
			Camera.main.transform.Rotate (0, Input.GetAxis ("Horizontal3"), 0);


		} else {

			//カメラの回転をリセットする
			//if (Input.GetButton ("CamReset"))
			//timer = 0.5f;

			//スムーズにカメラの回転を戻す
			//if (timer > 0) {
				CameraParent.transform.localRotation = Quaternion.Slerp (CameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 2);

				//timer -= Time.deltaTime;
			//}
		
		}

	}
}
