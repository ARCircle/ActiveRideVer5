using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	GameObject CameraParent;
	Quaternion defaultCameraRot;


	public float maxAngleH = 15; // 最大回転角度
	public float minAngleH = -15; // 最小回転角度

	public float maxAngleV = 15; // 最大回転角度
	public float minAngleV = -3; // 最小回転角度

	public float speed = 1.0f; // 回転スピード


	// Use this for initialization
	void Start () {

		//カメラ初期方向記憶
		CameraParent = Camera.main.transform.gameObject;
		defaultCameraRot = CameraParent.transform.localRotation;

	}

	// Update is called once per frame
	void Update () {


		if (Input.GetAxis ("Vertical3")!=0||Input.GetAxis ("Horizontal3")!=0) {
			//カメラを回転させる

			//Camera.main.transform.Rotate (-Input.GetAxis ("Vertical3"), 0, 0);
			//Camera.main.transform.Rotate (0, Input.GetAxis ("Horizontal3"), 0);

			// 入力情報.
			float turnH = Input.GetAxis("Horizontal3");
			// 現在の回転角度を0～360から-180～180に変換.
			float rotateY = (transform.eulerAngles.y > 180) ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
			// 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする.
			float angleY = Mathf.Clamp(rotateY + turnH * speed, minAngleH, maxAngleH);
			// 回転角度を-180～180から0～360に変換.
			angleY = (angleY < 0) ? angleY + 360 : angleY;

			// 入力情報.
			float turnV = Input.GetAxis("Vertical3");
			// 現在の回転角度を0～360から-180～180に変換.
			float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
			// 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする.
			float angleX = Mathf.Clamp(rotateX - turnV * speed, minAngleV, maxAngleV);
			// 回転角度を-180～180から0～360に変換.
			angleX = (angleX < 0) ? angleX + 360 : angleX;

			// 回転角度をオブジェクトに適用.
			transform.rotation = Quaternion.Euler(angleX, angleY, 0);


		} else {

			//スムーズにカメラの回転を戻す
				CameraParent.transform.localRotation = Quaternion.Slerp (CameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

		}

	}
}
