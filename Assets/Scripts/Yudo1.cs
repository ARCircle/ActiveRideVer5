using UnityEngine.UI;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class Yudo1: MonoBehaviour {

	private GameObject _target;
	Rigidbody _rigidbody;

	Vector3 _upEngine = Vector3.up*5;
	Vector3 _downEngine = Vector3.down*5;
	Vector3 _leftEngine = Vector3.left*5;
	Vector3 _rightEngine = Vector3.right*5;

	public float enginePower = 1000.0f;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
		_target = LockOn1.target;
	}

	// Update is called once per frame
	void Update () {
		//誘導
		Vector3 dif = _target.transform.position - transform.position;

		Vector3 difLocal = transform.InverseTransformVector(dif);

		float upPower;
		float downPower;
		float leftPower;
		float rightPower;

		float difX = difLocal.x;
		if( difX > 5 )
		{
			leftPower = 1.1f;
			rightPower = 0.9f;
		}
		else if( difX < -5 )
		{
			leftPower = 0.9f;
			rightPower = 1.1f;
		}
		else
		{
			leftPower = rightPower = 1.0f;
		}

		float difY = difLocal.y;
		if( difY > 5 )
		{
			upPower = 0.9f;
			downPower = 1.1f;
		}
		else if( difY < -5 )
		{
			upPower = 1.1f;
			downPower = 0.9f;
		}
		else
		{
			upPower = downPower = 1.0f;
		}

		Vector3 forward = transform.forward * 3;
		_rigidbody.AddForceAtPosition(enginePower * upPower * forward, transform.TransformPoint( _upEngine ), ForceMode.Force);
		_rigidbody.AddForceAtPosition(enginePower * downPower * forward, transform.TransformPoint( _downEngine ), ForceMode.Force);
		_rigidbody.AddForceAtPosition(enginePower * leftPower * forward, transform.TransformPoint( _leftEngine ), ForceMode.Force);
		_rigidbody.AddForceAtPosition(enginePower * rightPower * forward, transform.TransformPoint( _rightEngine ), ForceMode.Force);
	}
	/*
	//一番近い敵を探して取得
	GameObject FindClosestEnemy() {

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in gos) {

			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}

		if (closest != null) {

			//一番近くの敵がロックオン範囲外ならロックしない
			if (Vector3.Distance (closest.transform.position, transform.position) > 100)
				closest = null;
		}

		return closest;
	}*/
}