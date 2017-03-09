using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour {

	//public：地点AとB
	public Transform From,To;
	//public：秒数
	public float Sec=3.0f;
	//
	float Bunshi=0;
	//public：アニメーション再生
	public bool StartTween=false;
	//public：アニメーションリセット
	public bool ResetTween=false;

	float timer;
	public float starttime = 0;


	void Start () {
		
		timer = 0;

		//地点Aを初期位置へ
		transform.position = From.position;
	}



	void Update () {

		timer += Time.deltaTime;

		if (timer > starttime) {

			//A-B差分と分子にA地点の座標を足して現在位置を算出
			transform.position = From.position + Vector3.Scale (
				To.position - From.position, new Vector3 (Bunshi, Bunshi, Bunshi));
			//StartTweenがONのとき･･･
			if (StartTween == true) {
				//分子に秒単位÷指定秒数を加算
				Bunshi += Time.deltaTime / Sec;
				//分子が１以上のとき･･･
				if (Bunshi >= 1f) {
					//再生終了。
					Bunshi = 1f;
					StartTween = false;
				}
			}
			//リセット機能
			if (ResetTween == true) {
				Bunshi = 0;
				ResetTween = false;
			}
		}
	}
}
