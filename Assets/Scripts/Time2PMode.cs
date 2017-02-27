using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Time2PMode : MonoBehaviour {
	public Text time;
	public static float timeLimit2P;

	// Use this for initialization
	void Start () {
		timeLimit2P = 120.0f;

	}

	// Update is called once per frame
	void Update () {

		//残り時間の処理
		timeLimit2P -= Time.deltaTime;
		if (timeLimit2P >= 30) {
			time.text = " Time : " + timeLimit2P.ToString ("f2") + " ";
		}
		if (timeLimit2P <= 30) {
			time.color = Color.red;
			time.text = " Time : " + timeLimit2P.ToString ("f2") + " ";
		}

		if (timeLimit2P <= 0) {
			SceneManager.LoadScene ("Result");

		}
	}


}
