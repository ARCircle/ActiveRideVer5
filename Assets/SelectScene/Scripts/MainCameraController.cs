using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	public int MaxLane;
	public int MinLane;
	public float speed;
	public float LaneWidth;

	CharacterController controller;

	int targetLane1;

	Vector3 moveDirection = Vector3.zero;


	void Start () {

		controller = GetComponent<CharacterController> ();
		targetLane1 = DoublePlayerSelectController.Selectnumber1 ();
	}
	

	void Update () {

		targetLane1 = DoublePlayerSelectController.Selectnumber1 ();

		float ratioX = (targetLane1 * LaneWidth - transform.position.x) / LaneWidth;
		moveDirection.x = ratioX * speed;

		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);
	
	}
}
