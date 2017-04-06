using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutinManager_hissatu : MonoBehaviour {

	public Camera resultCamera;

	//カットインするオブジェクト群を格納
	public List<GameObject> CutInObjects = new List<GameObject>();

	//カットインの起点と終点
	private Vector3 CutInPosFrom = new Vector3(1000f,0,0);
	private Vector3 CutInPosTo = new Vector3(0f,0,0);
	//カットインするオブジェクトの位置
	private Vector3 CutInPos;

	public static bool canCutIn;
	private bool canStopPlayerMove;

	private float TimeLeft;
	private float TimeLeft_PausePlayer;
	private float TimerWait;

	private float DeltaTime;

	//カットインを行う時間
	public float TimeLeft_MAX = 0.5f;
	private float CutInDelta;

	public Image backimg;

	//Canvasとか低い階層にアタッチ
	//canCutInをPauserScript.csに渡して止めてるので, PauserScriptもIsModalOptionがfalseになるようにアタッチ

	// Use this for initialization
	void Start () {

		backimg.enabled = false;

		resultCamera.enabled = false;
		PlayerShoot_U.Shoot2OK = 1;


		foreach(GameObject cutin in CutInObjects)
		{
			cutin.SetActive(false);
		}
		canCutIn = false;
		canStopPlayerMove = false;

		TimeLeft = TimeLeft_MAX;
		TimeLeft_PausePlayer = 2.0f;

		DeltaTime = Time.deltaTime;

		CutInPos = CutInPosFrom;

		CutInDelta = TimeLeft_MAX / Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {


		if (PlayerShoot_U.Shoot2OK == 2) {
			if (Input.GetButton("ShootMode1") && Input.GetButton("Fire1")) {
				TimerWait += Time.deltaTime;
				if (TimerWait >= 0.01F) {
					//カットイン起こしたいトリガ処理に応じてCutInChecker(GameObject CutInTarget)に
					//カットイン対象のGameObjectを投げる
					//(この場合にはCutInObjects[0]に格納したものの1つ, Start関数ですべてSetActive(false)にしてる)
					CutInChecker (CutInObjects [0]);
					TimerWait = 0;
					PlayerShoot_U.Shoot2OK = 1;
				}
			}
		}


		if (canCutIn)
		{
			//CutInObjects内でSetActiveがCutInChecker(GameObject CutInTarget)にてtrueに指定, 
			//かつカットインを起こせるか否かのフラグがtrueのときのみ実行される
			foreach (GameObject targetObject in CutInObjects)
			{
				if (targetObject.activeSelf)
				{
					DoCutIn(targetObject);
				}
			}

		}

		Debug.Log (TimeLeft_PausePlayer + "," + canStopPlayerMove );
		if (canStopPlayerMove) {
			if (TimeLeft_PausePlayer >= 0f)
			{
				TimeLeft_PausePlayer -= Time.deltaTime;

				GetComponentInParentAndChildren<PlayerMove> (this.gameObject).enabled = false;
				GetComponentInParentAndChildren<Animator>(this.gameObject).enabled = false;
			}
			else
			{
				TimeLeft_PausePlayer = 2.0f;
				canStopPlayerMove = false;

				GetComponentInParentAndChildren<PlayerMove> (this.gameObject).enabled = true;
				GetComponentInParentAndChildren<Animator> (this.gameObject).enabled = true;
			}		
		}
	}

	public void CutInChecker(GameObject CutInTarget)
	{
		if (canCutIn)
		{
			CutInTarget.SetActive(false);
			canCutIn = false;
		}
		else
		{
			CutInTarget.SetActive(true);
			canCutIn = true;
		}
	}

	public void DoCutIn(GameObject CutInTarget)
	{
		if (TimeLeft >= 0f)
		{
			CutInPos.x = CutInPos.x - CutInPosFrom.x / CutInDelta;
			CutInTarget.transform.localPosition = CutInPos;
			TimeLeft -= DeltaTime;
			resultCamera.enabled = true;
			backimg.enabled = true;
		}
		else
		{
			TimeLeft = TimeLeft_MAX;
			CutInPos.x = CutInPosFrom.x;
			CutInTarget.transform.localPosition = CutInPosFrom;
			CutInTarget.SetActive(false);

			canCutIn = false;
			resultCamera.enabled = false;
			backimg.enabled = false;
			canStopPlayerMove = true;

		}
	}

	//  GameObjectExtension.cs
	//  http://kan-kikuchi.hatenablog.com/entry/GetComponentInParentAndChildren
	//
	//  Created by kikuchikan on 2015.08.25.

	/// <summary>
	/// 親や子オブジェクトも含めた範囲から指定のコンポーネントを取得する
	/// </summary>
	public static T GetComponentInParentAndChildren<T>(GameObject gameObject)
	{

		if (gameObject.GetComponentInParent<T>() != null)
		{
			return gameObject.GetComponentInParent<T>();
		}
		if (gameObject.GetComponentInChildren<T>() != null)
		{
			return gameObject.GetComponentInChildren<T>();
		}

		return gameObject.GetComponent<T>();
	}

}