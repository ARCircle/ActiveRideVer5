using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public float speed;
	public float LaneWidth;

	CharacterController controller;

	int targetLane;

	Vector3 moveDirection = Vector3.zero;


	void Start () {

		controller = GetComponent<CharacterController> ();
		targetLane = PlayerSelectController.Selectnumber ();
	}


	void Update () {

		targetLane = PlayerSelectController.Selectnumber ();

		float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
		moveDirection.x = ratioX * speed;

		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

	}
}