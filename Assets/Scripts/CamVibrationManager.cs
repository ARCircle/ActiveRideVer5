using UnityEngine;
using System.Collections;

public class CamVibrationManager : MonoBehaviour {

	public static float vibration;
	Vector3 defaultPosition;
	
	// Use this for initialization
	void Start () {
	
		vibration = 0;
		//初期値を記憶しておく
		defaultPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
		vibration = Mathf.Clamp(vibration, 0, 0.5F);
	
		if (vibration > 0) {
			
			//カメラのポジションにランダム値を入れて揺らす
			Vector3 randomPosition;
			randomPosition.x = Random.Range (vibration * -1, vibration);
			randomPosition.y = Random.Range (vibration * -1, vibration);
			randomPosition.z = Random.Range (vibration * -1, vibration);
			
			transform.localPosition = defaultPosition + randomPosition;
			
			vibration -= Time.deltaTime;
		} else {
			transform.localPosition = defaultPosition;
		}
	}
}
