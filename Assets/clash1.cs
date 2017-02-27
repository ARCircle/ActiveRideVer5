using UnityEngine;
using System.Collections;

public class clash1 : MonoBehaviour{

	public float setshakeTime;
	float shakerange;

	private float lifeTime;
	private Vector3 savePosition;
	private float lowRangeX;
	private float maxRangeX;
	private float lowRangeY;
	private float maxRangeY;

	float time1 = 0.0f;
	float time2 = 0.0f;

	void catchShake(){

		savePosition = transform.position;
		lowRangeY = savePosition.y - shakerange;
		maxRangeY = savePosition.y + shakerange;
		lowRangeX = savePosition.x - shakerange;
		maxRangeX = savePosition.x + shakerange;
		lifeTime = setshakeTime;
	}

	void Start(){

		if (setshakeTime <= 0.0f)
			setshakeTime = 0.1f;
		lifeTime = 0.0f;
	}

	void Update(){

		int flag = camera1Controller.flag3 ();

		if (flag == 0) {
			time1 += Time.deltaTime;
			shakerange = 0.01f*time1;
		} else {
			time2 += Time.deltaTime;
			if (time2 >= 1.0f)
				shakerange = -0.13f * time2 + 0.15f;
			else
				shakerange = -0.02f / 3.0f * time2 + 0.08f / 3.0f;
		}

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

		//if (Input.GetKeyDown ("space"))
			catchShake ();
	}
}