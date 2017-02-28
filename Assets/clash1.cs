using UnityEngine;
using System.Collections;

public class clash1 : MonoBehaviour{

	public float setshakeTime;
	float shakerange = 0.15f;

	private float lifeTime;
	private Vector3 savePosition;
	private float lowRangeX;
	private float maxRangeX;
	private float lowRangeY;
	private float maxRangeY;

	float time;

	void catchShake(){

		savePosition = transform.position;
		lowRangeY = savePosition.y - shakerange;
		maxRangeY = savePosition.y + shakerange;
		lowRangeX = savePosition.x - shakerange;
		maxRangeX = savePosition.x + shakerange;
		lifeTime = setshakeTime;

		time = 0.0f;
	}

	void Start(){

		if (setshakeTime <= 0.0f)
			setshakeTime = 0.1f;
		lifeTime = 0.0f;
	}

	void Update(){

		//time += Time.deltaTime;

		//shakerange = 0.01f * time;

		/*
		if (time >= 3.5f) {
			shakerange = 0.55f * time-1.89f;
		}

		if (time >= 3.8f) {
			shakerange = -0.01167f * time + 0.01143f;
		}*/


		if(lifeTime < 0.0f){
			transform.position = savePosition;
			lifeTime = 0.0f;
		}

		if(lifeTime >0.0f){
			lifeTime -= Time.deltaTime;
			float x_val = Random.Range (lowRangeX, maxRangeX);
			float y_val = Random.Range (lowRangeY, maxRangeY);
			transform.position = new Vector3 (x_val, y_val, transform.position.z);
		}
			
		catchShake ();
	}
}