using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class LockOn2 : MonoBehaviour {

	public static GameObject target;

	public Image lockOnImage;

	public Camera MainCamera2;

	public GameObject enemyAp;
	public Image gaugeImage;
	public Text textDistance;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

		//ターゲットを取得する
		target = GameObject.Find("PlayerTarget1");

		//スムーズにターゲットの方向を向く
		Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);
		transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

		//カメラをターゲットに向ける
		Transform cameraParent = MainCamera2.transform.parent;
		Quaternion targetRotation2 = Quaternion.LookRotation (target.transform.position - cameraParent.position);
		cameraParent.localRotation = Quaternion.Slerp (cameraParent.localRotation, targetRotation2, Time.deltaTime * 10);
		cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);

		//ターゲットの表示位置にロックオンカーソルを合わせる
		lockOnImage.transform.position = MainCamera2.WorldToScreenPoint(target.transform.position);

		//敵の体力をゲージに反映させる
		gaugeImage.transform.localScale = new Vector3( (float) PlayerAp1.armorPoint / PlayerAp1.armorPointMax, 1, 1);

		//敵との距離を表示する
		textDistance.text = Vector3.Distance (target.transform.position, transform.position).ToString();

	}
}
