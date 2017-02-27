using UnityEngine;
using System.Collections;

public class ExplosionCllider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.tag == "Player") {
			
			//プレイヤーと衝突したら「Intensity」の値を送る
			//ScreenOverlayManager.intensity += 1;
			
			//距離によって送る「Intensity」の値を変える
			float distance = Vector3.Distance (collider.transform.position, transform.position);
			ScreenOverlayManager.intensity += Mathf.Clamp(1 - (distance / 200), 0, 1);
			
			//プレイヤーと衝突したら「vibration」の値を送る
			CamVibrationManager.vibration += Mathf.Clamp(0.5F - (distance / 200), 0, 0.5F);

			Destroy (gameObject);
		}
	}
}
