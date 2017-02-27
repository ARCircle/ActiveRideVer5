using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Marker1 : MonoBehaviour {

	Image marker;
	public Image markerImage;
	GameObject compass;
	
	GameObject target;
	
	// Use this for initialization
	void Start () {
		
		target = GameObject.Find("PlayerTarget2");
	
		//マーカーをレーダー（コンパス）上に表示する
		compass = GameObject.Find ("CompassMask2");
		marker = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
		marker.transform.SetParent(compass.transform, false);
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//マーカーをプレイヤーの相対位置に配置する
		Vector3 position = transform.position - target.transform.position;
		marker.transform.localPosition = new Vector3 (position.x, position.z, 0);
		
		/*
		//レーダーの範囲外に出たら表示しない
		if (Vector3.Distance (target.transform.position, transform.position) <= 150)
			marker.enabled = true;
		else
			marker.enabled = false;
		*/
	}
	
	//敵が消滅したらマーカーも消滅させる
	void OnDestroy() {
		
		Destroy(marker);
	}
}
