using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostController : MonoBehaviour {


	Vector3 startPosition;

	public float speed;
	float time;

	void Start () {
		startPosition = transform.localPosition;
		time = 0.0f;
	}
	

	void Update () {

		int flag = motionController.movflag ();

		if (flag == 1) {
		
			time += Time.deltaTime;

			float z = -time * speed;

			transform.localPosition = startPosition + new Vector3 (0, 0, z);
		
		}
		
	}
}
