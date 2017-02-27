using UnityEngine;
using System.Collections;

public class MainCameraController2 : MonoBehaviour {

	public int MaxLane;
	public int MinLane;
	public float speed;
	public float LaneWidth;

	CharacterController controller;

	int targetLane2;

	Vector3 moveDirection = Vector3.zero;


	void Start () {

		controller = GetComponent<CharacterController> ();
		targetLane2 = DoublePlayerSelectController.Selectnumber2 ();
	}


	void Update () {

		targetLane2 = DoublePlayerSelectController.Selectnumber2 ();

		float ratioX = (targetLane2* LaneWidth - transform.position.x) / LaneWidth;
		moveDirection.x = ratioX * speed;

		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

	}
}

