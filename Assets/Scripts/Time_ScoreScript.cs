using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Time_ScoreScript : MonoBehaviour {
	public Text time;
	public Text score;
	public float TimeLimit_1P;
	public static float timeLimit_1P_tmp;
	public static float timeLimit_tmp;
	public static int destroyScore;
	public static int timeScore;
	public static int HPScore;
	public static int allScore;
	public static int tmp_destroyScore;

	// Use this for initialization
	void Start () {
		timeLimit_1P_tmp = TimeLimit_1P;
		timeLimit_tmp = timeLimit_1P_tmp;
		destroyScore = 0;
		timeScore = 0;
		HPScore = 0;
		allScore = 0;

	}

	// Update is called once per frame
	void Update () {

		//残り時間の処理
		timeLimit_tmp -= Time.deltaTime;
		if (timeLimit_tmp >= 30) {
			time.text = " Time : " + timeLimit_tmp.ToString ("f2") + " ";
		}
		if (timeLimit_tmp <= 30) {
			time.color = Color.red;
			time.text = " Time : " + timeLimit_tmp.ToString ("f2") + " ";
		}

		if (timeLimit_tmp <= 0) {
			SceneManager.LoadScene ("Result");
	

		}

		//Scoreの処理
		//destroyscoreはEnemy.csで管理

		allScore = destroyScore + HPScore + timeScore;
		score.text = string.Format("Score : {0:0000000}", allScore);


	}
		
		
}
